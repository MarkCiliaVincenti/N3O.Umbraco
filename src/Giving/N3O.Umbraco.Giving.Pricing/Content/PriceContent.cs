﻿using N3O.Umbraco.Attributes;
using N3O.Umbraco.Content;
using N3O.Umbraco.Giving.Pricing.Models;

namespace N3O.Umbraco.Giving.Pricing.Content {
    public class PriceContent : UmbracoContent<PriceContent>, IPrice {
        [UmbracoProperty("priceAmount")]
        public decimal Amount => GetValue(x => x.Amount);

        [UmbracoProperty("priceLocked")]
        public bool Locked => GetValue(x => x.Locked);
    }
}