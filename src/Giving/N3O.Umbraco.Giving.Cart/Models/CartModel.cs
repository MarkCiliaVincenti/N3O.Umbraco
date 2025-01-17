using N3O.Umbraco.Content;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Financial;
using N3O.Umbraco.Giving.Content;
using N3O.Umbraco.Localization;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Extensions;

namespace N3O.Umbraco.Giving.Cart.Models;

public class CartModel {
    private static readonly string CheckoutPageAlias = "checkoutPage";
    private static readonly string DonatePageAlias = AliasHelper<DonatePageContent>.ContentTypeAlias();
    
    private readonly IFormatter _formatter;

    public CartModel(IFormatter formatter,
                     IContentCache contentCache,
                     Currency currency,
                     CartContents donation,
                     CartContents regularGiving,
                     UpsellModel upsell,
                     bool checkoutView) {
        _formatter = formatter;
        Currency = currency;
        Donation = donation;
        RegularGiving = regularGiving;
        Upsell = upsell;
        CheckoutView = checkoutView;
        TotalText = GetTotalText(donation, regularGiving);
        TotalItems = donation.Allocations.Count() + regularGiving.Allocations.Count();
        CheckoutUrl = contentCache.Single(CheckoutPageAlias)?.Url();
        DonateUrl = contentCache.Single(DonatePageAlias)?.Url();
    }

    public Currency Currency { get; }
    public CartContents Donation { get; }
    public CartContents RegularGiving { get; }
    public UpsellModel Upsell { get; }
    public bool CheckoutView { get; }
    public string TotalText { get; }
    public int TotalItems { get; }
    public string CheckoutUrl { get; }
    public string DonateUrl { get; }

    public bool ContainsUpsell() {
        return Donation.OrEmpty(x => x.Allocations).Any(x => x.Upsell);
    }

    public bool IsEmpty() => Donation.IsEmpty() && RegularGiving.IsEmpty();

    private string GetTotalText(CartContents single, CartContents regular) {
        var totals = new List<string>();

        if (!single.Total.IsZero()) {
            totals.Add(_formatter.Text.Format<Strings>(s => s.DonationTotal,
                                                       _formatter.Number.FormatMoney(single.Total)));
        }
    
        if (!regular.Total.IsZero()) {
            totals.Add(_formatter.Text.Format<Strings>(s => s.RegularGivingTotal,
                                                       _formatter.Number.FormatMoney(regular.Total)));
        }

        return string.Join(" + ", totals);
    }

    public class Strings : CodeStrings {
        public string DonationTotal => "{0}";
        public string RegularGivingTotal => "{0} / month";
    }
}
