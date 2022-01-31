using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Composing;
using N3O.Umbraco.Extensions;
using Umbraco.Cms.Core.DependencyInjection;

namespace N3O.Umbraco.Giving.Checkout {
    public class CheckoutComposer : Composer {
        public override void Compose(IUmbracoBuilder builder) {
            builder.Services.AddTransient<ICheckoutIdAccessor, CheckoutIdAccessor>();
            builder.Services.AddSingleton<ICheckoutReceipt, CheckoutReceipt>();

            builder.Services.AddOpenApiDocument(CheckoutConstants.ApiName);
        }
    }
}