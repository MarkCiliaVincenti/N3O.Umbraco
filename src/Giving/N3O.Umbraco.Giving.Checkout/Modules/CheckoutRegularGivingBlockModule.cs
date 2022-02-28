﻿using N3O.Umbraco.Blocks;
using N3O.Umbraco.Content;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Giving.Checkout.Content;
using N3O.Umbraco.Giving.Checkout.Models;
using N3O.Umbraco.Lookups;
using N3O.Umbraco.Payments.Lookups;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace N3O.Umbraco.Giving.Checkout.Modules {
    public class CheckoutRegularGivingBlockModule : IBlockModule {
        private static readonly string BlockAlias = AliasHelper<CheckoutRegularGivingBlockContent>.ContentTypeAlias();
        
        private readonly ICheckoutAccessor _checkoutAccessor;
        private readonly IContentCache _contentCache;
        private readonly ILookups _lookups;

        public CheckoutRegularGivingBlockModule(ICheckoutAccessor checkoutAccessor,
                                           IContentCache contentCache,
                                           ILookups lookups) {
            _checkoutAccessor = checkoutAccessor;
            _contentCache = contentCache;
            _lookups = lookups;
        }
        
        public bool ShouldExecute(IPublishedElement block) {
            return block.ContentType.Alias.EqualsInvariant(BlockAlias);
        }

        public Task<object> ExecuteAsync(IPublishedElement block, CancellationToken cancellationToken) {
            var checkout = _checkoutAccessor.Get();

            var paymentMethods = _lookups.GetAll<PaymentMethod>()
                                         .Where(x => x.IsAvailable(_contentCache,
                                                                   checkout.Account.Address.Country,
                                                                   checkout.Currency) &&
                                                     x.SupportsCredentials)
                                         .ToList();

            return Task.FromResult<object>(new CheckoutRegularGivingModel(checkout, paymentMethods));
        }
        
        public string Key => CheckoutConstants.BlockModuleKeys.CheckoutRegularGiving;
    }
}