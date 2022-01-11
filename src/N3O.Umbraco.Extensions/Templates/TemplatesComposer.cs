﻿using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Composing;
using N3O.Umbraco.Partials;
using Umbraco.Cms.Core.DependencyInjection;

namespace N3O.Umbraco.Templates {
    public class TemplatesComposer : Composer {
        public override void Compose(IUmbracoBuilder builder) {
            builder.Services.AddTransient<IPartialText, PartialText>();
            builder.Services.AddScoped<IStyleContext, StyleContext>();
            builder.Services.AddTransient<ITemplateText, TemplateText>();
        }
    }
}