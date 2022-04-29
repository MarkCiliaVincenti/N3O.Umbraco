﻿using N3O.Umbraco.Content;
using N3O.Umbraco.Data.Commands;
using N3O.Umbraco.Data.Converters;
using N3O.Umbraco.Data.Exceptions;
using N3O.Umbraco.Data.Extensions;
using N3O.Umbraco.Data.Filters;
using N3O.Umbraco.Data.Konstrukt;
using N3O.Umbraco.Data.Lookups;
using N3O.Umbraco.Data.Models;
using N3O.Umbraco.Data.Parsing;
using N3O.Umbraco.Data.Providers;
using N3O.Umbraco.Data.Services;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Json;
using N3O.Umbraco.Localization;
using N3O.Umbraco.Mediator;
using N3O.Umbraco.References;
using N3O.Umbraco.Storage.Extensions;
using N3O.Umbraco.Storage.Services;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Extensions;

namespace N3O.Umbraco.Data.Handlers {
    public class QueueImportsHandler : IRequestHandler<QueueImportsCommand, QueueImportsReq, QueueImportsRes> {
        private const string CountersKey = "Import";
        private const int MaxRowsCount = 5_000;

        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        private readonly IWorkspace _workspace;
        private readonly ILocalClock _clock;
        private readonly ICounters _counters;
        private readonly IJsonProvider _jsonProvider;
        private readonly IContentHelper _contentHelper;
        private readonly IContentService _contentService;
        private readonly IContentTypeService _contentTypeService;
        private readonly IDataTypeService _dataTypeService;
        private readonly IUmbracoDatabaseFactory _umbracoDatabaseFactory;
        private readonly IImportProcessingQueue _importProcessingQueue;
        private readonly Lazy<IVolume> _volume;
        private readonly ITempStorage _tempStorage;
        private readonly ErrorLog _errorLog;
        private readonly IReadOnlyList<IPropertyConverter> _converters;
        private readonly IReadOnlyList<IImportPropertyFilter> _propertyFilters;
        private readonly List<IContentMatcher> _contentMatchers;
        private IReadOnlyList<IContent> _descendants;

        public QueueImportsHandler(IBackOfficeSecurityAccessor backOfficeSecurityAccessor,
                                   IWorkspace workspace,
                                   ILocalClock clock,
                                   ICounters counters,
                                   IJsonProvider jsonProvider,
                                   IContentHelper contentHelper,
                                   IContentService contentService,
                                   IContentTypeService contentTypeService,
                                   IDataTypeService dataTypeService,
                                   IUmbracoDatabaseFactory umbracoDatabaseFactory,
                                   IImportProcessingQueue importProcessingQueue,
                                   Lazy<IVolume> volume,
                                   ITempStorage tempStorage,
                                   IFormatter formatter,
                                   IEnumerable<IPropertyConverter> converters,
                                   IEnumerable<IImportPropertyFilter> propertyFilters,
                                   IEnumerable<IContentMatcher> contentMatchers) {
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
            _workspace = workspace;
            _clock = clock;
            _counters = counters;
            _jsonProvider = jsonProvider;
            _contentHelper = contentHelper;
            _contentService = contentService;
            _contentTypeService = contentTypeService;
            _dataTypeService = dataTypeService;
            _umbracoDatabaseFactory = umbracoDatabaseFactory;
            _importProcessingQueue = importProcessingQueue;
            _volume = volume;
            _tempStorage = tempStorage;
            _errorLog = new ErrorLog(formatter);
            _converters = converters.OrEmpty().ToList();
            _propertyFilters = propertyFilters.OrEmpty().ToList();
            _contentMatchers = contentMatchers.OrEmpty().ToList();
        }
        
        public async Task<QueueImportsRes> Handle(QueueImportsCommand req, CancellationToken cancellationToken) {
            try {
                var count = await QueueAsync(req, cancellationToken);

                return new QueueImportsRes {
                    Success = true,
                    Count = count
                };
            } catch (ProcessingException processingException) {
                return new QueueImportsRes {
                    Success = false,
                    Errors = processingException.Errors
                };
            } catch (Exception ex) {
                return new QueueImportsRes {
                    Success = false,
                    Errors = ex.ToString().Yield()
                };
            }
        }
        
        private async Task<int> QueueAsync(QueueImportsCommand req, CancellationToken cancellationToken) {
            var csvBlob = await _tempStorage.GetFileAsync(req.Model.CsvFile.Filename, cancellationToken);

            try {
                using (csvBlob.Stream) {
                    var containerContent = req.ContentId.Run(_contentService.GetById, true);
                    var contentType = _contentTypeService.GetContentTypeForContainerContent(containerContent.ContentTypeId);

                    var propertyInfos = contentType.GetUmbracoProperties(_dataTypeService).ToList();
                    var propertyInfoColumns = propertyInfos.Where(x => x.CanInclude(_propertyFilters))
                                                           .ToDictionary(x => x,
                                                                         x => _workspace.ColumnRangeBuilder
                                                                                        .GetColumns(x.GetTemplateColumn(_converters)));

                    var csvReader = _workspace.GetCsvReader(req.Model.DatePattern,
                                                            DecimalSeparators.Point,
                                                            BlobResolvers.Url(),
                                                            TextEncodings.Utf8,
                                                            csvBlob.Stream,
                                                            true);

                    ValidateColumns(csvReader.GetColumnHeadings(), propertyInfoColumns.SelectMany(x => x.Value));

                    var currentUser = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser;
                    var imports = new List<Import>();
                    var batchReference = (int) await _counters.NextAsync(CountersKey, 100_001, cancellationToken);
                    var batchFilename = csvBlob.Filename;
                    var queuedAt = _clock.GetLocalNow().ToDateTimeUnspecified();
                    var canReplace = csvReader.GetColumnHeadings().Contains(DataConstants.Columns.Replaces, true);
                    var storageFolderName = req.Model.ZipFile.HasValue() ? $"Import{batchReference}" : null;
                    var contentMatcher = _contentMatchers.SingleOrDefault(x => x.IsMatcher(contentType.Alias));
                    var parserSettings =  _jsonProvider.SerializeObject(new ParserSettings(req.Model.DatePattern,
                                                                                           DecimalSeparators.Point,
                                                                                           storageFolderName));

                    if (req.Model.ZipFile != null) {
                        await ExtractToStorageFolderAsync(req.Model.ZipFile, storageFolderName);
                    }

                    var rowNumber = 1;
                    while (csvReader.ReadRow()) {
                        var import = new Import();
                        import.Reference = $"{batchReference}-{rowNumber}";
                        import.QueuedAt = queuedAt;
                        import.QueuedByUser = currentUser.Key;
                        import.QueuedByName = currentUser.Name;
                        import.BatchReference = batchReference.ToString();
                        import.BatchFilename = batchFilename;
                        import.FileRowNumber = rowNumber;
                        import.ParserSettings = parserSettings;
                        import.ContentTypeAlias = contentType.Alias;
                        import.ParentId = containerContent.Key;
                        import.ContentTypeName = contentType.Name;
                        
                        var replacesCriteria = canReplace
                                                   ? csvReader.Row.GetRawField(DataConstants.Columns.Replaces)
                                                   : null;

                        if (replacesCriteria.HasValue()) {
                            import.Action = ImportActions.Update;
                            import.ReplacesId = FindExistingId(containerContent,
                                                               contentType.Alias,
                                                               contentMatcher,
                                                               replacesCriteria);
                        } else {
                            import.Action = ImportActions.Create;
                        }

                        import.Fields = _jsonProvider.SerializeObject(GetFields(csvReader, propertyInfoColumns));
                        import.Status = ImportStatuses.Queued;

                        imports.Add(import);

                        rowNumber++;
                    }

                    ValidateImports(imports);

                    await InsertAndQueueAsync(imports);

                    return imports.Count;
                }
            } finally {
                await _tempStorage.DeleteFileAsync(csvBlob.Filename);
            }
        }

        private void ValidateColumns(IReadOnlyList<string> csvHeadings, IEnumerable<Column> expectedColumns) {
            var expectedHeadings = expectedColumns.Select(x => x.Title).ToList();
            var missingHeadings = expectedHeadings.Except(csvHeadings, StringComparer.InvariantCultureIgnoreCase)
                                                  .ToList();

            if (missingHeadings.Any()) {
                foreach (var missingHeading in missingHeadings) {
                    _errorLog.AddError<Strings>(s => s.MissingColumn_1, missingHeading);
                }
            }
            
            _errorLog.ThrowIfHasErrors();
        }
        
        private void ValidateImports(IReadOnlyList<Import> imports) {
            if (imports.Count > MaxRowsCount) {
                _errorLog.AddError<Strings>(s => s.MaxRowsExceeded_1, MaxRowsCount);
            }
            
            _errorLog.ThrowIfHasErrors();
        }

        private async Task ExtractToStorageFolderAsync(StorageToken zipStorageToken, string storageFolderName) {
            var zipBlob = await _tempStorage.GetFileAsync(zipStorageToken.Filename);

            try {
                using (zipBlob.Stream) {
                    var storageFolder = await _volume.Value.GetStorageFolderAsync(storageFolderName);
                    var zipArchive = new ZipArchive(zipBlob.Stream, ZipArchiveMode.Read);
                    
                    await zipArchive.ExtractToStorageFolderAsync(storageFolder);
                }
            } finally {
                await _tempStorage.DeleteFileAsync(zipBlob.Filename);
            }
        }

        private Guid? FindExistingId(IContent container,
                                     string contentTypeAlias,
                                     IContentMatcher contentMatcher,
                                     string criteria) {
            if (contentMatcher == null) {
                _errorLog.AddError<Strings>(s => s.ContentMatcherNotFound_1, contentTypeAlias);

                return null;
            }

            if (_descendants == null) {
                PopulateDescendants(container, contentTypeAlias);
            }

            var matches = new List<IContent>();
            foreach (var descendant in _descendants) {
                if (contentMatcher.IsMatch(descendant, criteria)) {
                    matches.Add(descendant);
                }
            }

            if (matches.IsSingle()) {
                return matches.Single().Key;
            } else {
                if (matches.None()) {
                    _errorLog.AddError<Strings>(s => s.NoContentMatched_1, criteria);   
                } else {
                    _errorLog.AddError<Strings>(s => s.MultipleContentMatched_1, criteria);
                }

                return null;
            }
        }

        private void PopulateDescendants(IContent container, string contentTypeAlias) {
            _descendants = _contentHelper.GetDescendants(container)
                                         .Where(x => x.ContentType.Alias.EqualsInvariant(contentTypeAlias))
                                         .ToList();
        }
        
        private IEnumerable<ImportField> GetFields(ICsvReader csvReader,
                                                   IReadOnlyDictionary<UmbracoPropertyInfo, IEnumerable<Column>> propertyColumns) {
            foreach (var (property, columns) in propertyColumns) {
                foreach (var column in columns) {
                    var field = new ImportField();
                    field.Property = property.Type.Alias;
                    field.Name = column.Title;
                    field.SourceValue = csvReader.Row.GetRawField(column.Title);
                    field.Value = field.SourceValue;
                    field.Ignore = false;

                    yield return field;
                }
            }
        }

        private async Task InsertAndQueueAsync(IReadOnlyList<Import> imports) {
            if (imports.Any()) {
                using (var db = _umbracoDatabaseFactory.CreateDatabase()) {
                    foreach (var import in imports) {
                        await db.InsertAsync(import);
                    }
                }

                _importProcessingQueue.AddAll(imports);
            }
        }
        
        public class Strings : CodeStrings {
            public string ContentMatcherNotFound_1 => $"No content matcher found for {"{0}".Quote()}";
            public string MaxRowsExceeded_1 => $"The CSV file contains more than the maximum allowed {0} rows";
            public string MissingColumn_1 => $"CSV file is missing column {"{0}".Quote()}";
            public string MultipleContentMatched_1 => $"More than one content found for {"{0}".Quote()}";
            public string NoContentMatched_1 => $"No content found for {"{0}".Quote()}";
        }
    }
}