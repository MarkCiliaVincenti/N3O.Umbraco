using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.ValueConverters;
using Newtonsoft.Json;
using Perplex.ContentBlocks.PropertyEditor;
using Perplex.ContentBlocks.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Persistence.Querying;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace N3O.Umbraco.Content {
    public class ContentHelper : IContentHelper {
        private readonly Lazy<IServiceProvider> _serviceProvider;
        private readonly Lazy<IContentService> _contentService;
        private readonly Lazy<IContentTypeService> _contentTypeService;
        private readonly Lazy<IContentLocator> _contentLocator;
        private readonly Lazy<IUmbracoContextFactory> _umbracoContextFactory;

        public ContentHelper(Lazy<IServiceProvider> serviceProvider,
                             Lazy<IContentService> contentService,
                             Lazy<IContentTypeService> contentTypeService,
                             Lazy<IContentLocator> contentLocator,
                             Lazy<IUmbracoContextFactory> umbracoContextFactory) {
            _serviceProvider = serviceProvider;
            _contentService = contentService;
            _contentTypeService = contentTypeService;
            _contentLocator = contentLocator;
            _umbracoContextFactory = umbracoContextFactory;
        }

        public IReadOnlyList<IContent> GetAncestors(IContent content) {
            var list = new List<IContent>();

            while (content.ParentId != -1) {
                content = _contentService.Value.GetById(content.ParentId);

                list.Add(content);
            }

            return list;
        }

        public IReadOnlyList<IContent> GetChildren(IContent content) {
            return GetAllPagedContent(content, _contentService.Value.GetPagedChildren);
        }

        public ContentBlocks GetContentBlocks(string contentTypeAlias, string propertyTypeAlias, object propertyValue) {
            var contentBlocks = GetConvertedValue<ContentBlocksValueConverter, ContentBlocks>(contentTypeAlias,
                                                                                              propertyTypeAlias,
                                                                                              propertyValue);

            return contentBlocks;
        }

        public ContentProperties GetContentProperties(IContent content) {
            var properties = content.Properties.Select(x => (x.PropertyType, x.GetValue()));
            
            return GetContentProperties(content.Key, content.ContentType.Alias, properties);
        }
        
        public ContentProperties GetContentProperties(Guid contentId,
                                                      string contentTypeAlias,
                                                      IEnumerable<(IPropertyType Type, object Value)> properties) {
            var contentProperties = new List<ContentProperty>();
            var nestedContentProperties = new List<NestedContentProperty>();
            var contentType = _contentTypeService.Value.Get(contentTypeAlias);

            foreach (var property in properties) {
                if (property.Type.IsNestedContent()) {
                    var json = (string) property.Value;
                    var nestedContent = JsonConvert.DeserializeObject(json);
                    var elements = GetContentPropertiessForNestedContent(nestedContent);
                    var nestedContentProperty = new NestedContentProperty(contentType,
                                                                          property.Type,
                                                                          elements,
                                                                          json);
                    
                    nestedContentProperties.Add(nestedContentProperty);
                } else if (property.Type.IsContentBlocks()) {
                    var json = (string) property.Value;
                    var blockContent = JsonConvert.DeserializeObject(json);
                    var elements = GetContentPropertiessForBlockContent(blockContent);
                    var nestedContentProperty = new NestedContentProperty(contentType,
                                                                          property.Type,
                                                                          elements,
                                                                          json);
                    
                    nestedContentProperties.Add(nestedContentProperty);
                } else {
                    contentProperties.Add(new ContentProperty(contentType, property.Type, property.Value));
                }
            }

            return new ContentProperties(contentId, contentTypeAlias, contentProperties, nestedContentProperties);
        }

        public TProperty GetConvertedValue<TConverter, TProperty>(string contentTypeAlias,
                                                                  string propertyTypeAlias,
                                                                  object propertyValue)
            where TConverter : class, IPropertyValueConverter {
            var umbracoContext = _umbracoContextFactory.Value.EnsureUmbracoContext().UmbracoContext;
            var converter = _serviceProvider.Value.GetRequiredService<TConverter>();
            var publishedContentType = umbracoContext.PublishedSnapshot.Content.GetContentType(contentTypeAlias);
            var publishedPropertyType = publishedContentType.GetPropertyType(propertyTypeAlias);
            
            var source = propertyValue;

            if (source == null) {
                return default;
            }

            var intermediate = converter.ConvertSourceToIntermediate(null, publishedPropertyType, source, false);
            var result = (TProperty) converter.ConvertIntermediateToObject(null,
                                                                           publishedPropertyType,
                                                                           PropertyCacheLevel.None,
                                                                           intermediate,
                                                                           false);

            return result;
        }
        
        public IReadOnlyList<IContent> GetDescendants(IContent content) {
            return GetAllPagedContent(content, _contentService.Value.GetPagedDescendants);
        }
        
        public IPublishedElement GetNestedContent(string contentTypeAlias,
                                                  string propertyTypeAlias,
                                                  object propertyValue) {
            var publishedElement = GetConvertedValue<NestedContentSingleValueConverter, IPublishedElement>(contentTypeAlias,
                                                                                                           propertyTypeAlias,
                                                                                                           propertyValue);

            return publishedElement;
        }
        
        public IReadOnlyList<IPublishedElement> GetNestedContents(string contentTypeAlias,
                                                                  string propertyTypeAlias,
                                                                  object propertyValue) {
            var publishedElements = GetConvertedValue<NestedContentManyValueConverter, IEnumerable<IPublishedElement>>(contentTypeAlias,
                                                                                                                       propertyTypeAlias,
                                                                                                                       propertyValue);

            return publishedElements.ToList();
        }

        public T GetPickerValue<T>(string contentTypeAlias,
                                   string propertyTypeAlias,
                                   object propertyValue) {
            var item = GetConvertedValue<StronglyTypedMultiNodeTreePickerValueConverter, T>(contentTypeAlias,
                                                                                            propertyTypeAlias,
                                                                                            propertyValue);

            return item;
        }

        public IReadOnlyList<T> GetPickerValues<T>(string contentTypeAlias,
                                                   string propertyTypeAlias,
                                                   object propertyValue) {
            var items = GetConvertedValue<StronglyTypedMultiNodeTreePickerValueConverter, IEnumerable<T>>(contentTypeAlias,
                                                                                                          propertyTypeAlias,
                                                                                                          propertyValue);

            return items.ToList();
        }

        public IReadOnlyList<T> GetPublishedAncestors<T>(IContent content) where T : IPublishedContent {
            return GetAncestors(content).Select(x => _contentLocator.Value.ById<T>(x.Key)).ToList();
        }
        
        public IReadOnlyList<T> GetPublishedChildren<T>(IContent content) where T : IPublishedContent {
            return GetChildren(content).Select(x => _contentLocator.Value.ById<T>(x.Key)).ToList();
        }
        
        public IReadOnlyList<T> GetPublishedDescendants<T>(IContent content) where T : IPublishedContent {
            return GetDescendants(content).Select(x => _contentLocator.Value.ById<T>(x.Key)).ToList();
        }
        
        private IReadOnlyList<IContent> GetAllPagedContent(IContent content, GetPagedContent getPagedContent) {
            var descendants = new List<IContent>();

            var startIndex = 0;
            var pageSize = 100;

            while (true) {
                descendants.AddRange(getPagedContent(content.Id, startIndex, pageSize, out var totalRecords));

                if ((startIndex + pageSize) <= totalRecords) {
                    break;
                }

                startIndex += pageSize;
            }

            return descendants;
        }

        private IReadOnlyList<ContentProperties> GetContentPropertiessForBlockContent(dynamic blockContent) {
            var contentPropertiess = new List<ContentProperties>();
            
            if (blockContent == null) {
                return contentPropertiess;
            }
            
            foreach (var block in blockContent.blocks) {
                contentPropertiess.AddRange(GetContentPropertiessForNestedContent(block.content));
            }

            return contentPropertiess;
        }

        private IReadOnlyList<ContentProperties> GetContentPropertiessForNestedContent(dynamic nestedContent) {
            var contentPropertiess = new List<ContentProperties>();

            if (nestedContent == null) {
                return contentPropertiess;
            }

            foreach (var content in nestedContent) {
                var id = Guid.Parse((string) content.key);
                var contentTypeAlias = (string) content.ncContentTypeAlias;
                var contentType = _contentTypeService.Value.Get(contentTypeAlias);

                var properties = new List<(IPropertyType, object)>();
                
                foreach (var propertyGroup in contentType.PropertyGroups) {
                    foreach (var propertyType in propertyGroup.PropertyTypes) {
                        var propertyValue = content[propertyType.Alias];

                        properties.Add((propertyType, propertyValue));
                    }
                }
                
                contentPropertiess.Add(GetContentProperties(id, contentTypeAlias, properties));
            }

            return contentPropertiess;
        }

        private delegate IEnumerable<IContent> GetPagedContent(int id,
                                                               long pageIndex,
                                                               int pageSize,
                                                               out long totalRecords,
                                                               IQuery<IContent> filter = null,
                                                               Ordering ordering = null);
    }
}
