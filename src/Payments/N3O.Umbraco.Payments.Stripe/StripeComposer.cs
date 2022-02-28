using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Composing;
using N3O.Umbraco.Content;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Payments.Stripe.Extensions;
using N3O.Umbraco.Payments.Stripe.Models;
using Stripe;
using Umbraco.Cms.Core.DependencyInjection;

namespace N3O.Umbraco.Payments.Stripe {
    public class StripeComposer : Composer {
        public override void Compose(IUmbracoBuilder builder) {
            builder.Services.AddOpenApiDocument(StripeConstants.ApiName);
            
            builder.Services.AddTransient<IPaymentMethodDataEntryConfiguration<StripePaymentMethod>, StripeDataEntryConfiguation>();

            builder.Services.AddTransient<StripeKeys>(serviceProvider => {
                var contentCache = serviceProvider.GetRequiredService<IContentCache>();
                var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
                var stripeKeys = contentCache.GetStripeKeys(webHostEnvironment);

                return stripeKeys;
            });
            
            builder.Services.AddTransient<StripeClient>(serviceProvider => {
                var apiKey = serviceProvider.GetRequiredService<StripeKeys>().Secret;

                return new StripeClient(apiKey);
            });
        }
    }
}