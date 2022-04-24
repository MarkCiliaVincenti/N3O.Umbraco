﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Composing;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Storage.Azure.Services;
using N3O.Umbraco.Storage.Services;
using System.Linq;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common.ApplicationBuilder;
using Umbraco.StorageProviders.AzureBlob;

namespace N3O.Umbraco.Storage.Azure {
    public class AzureStorageComposer : Composer {
        public override void Compose(IUmbracoBuilder builder) {
            builder.Services.AddSingleton<IVolume, AzureVolume>();
            
            var storageConfigured = builder.Config
                                           .GetSection("umbraco")
                                           .GetChildren()
                                           .Any(x => x.Key.EqualsInvariant("storage") &&
                                                     x.GetChildren().Any(c => c.Key.EqualsInvariant("azureBlob")));

            if (storageConfigured) {
                builder.AddAzureBlobMediaFileSystem();

                builder.Services.Configure<UmbracoPipelineOptions>(opt => {
                    var filter = new UmbracoPipelineFilter("AzureStorage");
                    filter.Endpoints = app => app.UseMiddleware<AzureBlobFileSystemMiddleware>();
            
                    opt.AddFilter(filter);
                });
            }
        }
    }
}