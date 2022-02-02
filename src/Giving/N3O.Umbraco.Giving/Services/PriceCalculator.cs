﻿using N3O.Giving.Models;
using N3O.Umbraco.Context;
using N3O.Umbraco.Extensions;
using N3O.Umbraco.Financial;
using N3O.Umbraco.Forex;
using N3O.Umbraco.Giving.Extensions;
using N3O.Umbraco.Giving.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace N3O.Umbraco.Giving {
    public class PriceCalculator : IPriceCalculator {
        private readonly IBaseCurrencyAccessor _baseCurrencyAccessor;
        private readonly ICurrencyAccessor _currencyAccessor;
        private readonly IForexConverter _forexConverter;

        public PriceCalculator(IBaseCurrencyAccessor baseCurrencyAccessor,
                               ICurrencyAccessor currencyAccessor,
                               IForexConverter forexConverter) {
            _baseCurrencyAccessor = baseCurrencyAccessor;
            _currencyAccessor = currencyAccessor;
            _forexConverter = forexConverter;
        }
    
        public Price InBaseCurrency(IPricing pricing, IFundDimensionValues fundDimensions) {
            var baseCurrency = _baseCurrencyAccessor.GetBaseCurrency();

            return InCurrencyAsync(pricing, fundDimensions, baseCurrency).GetAwaiter().GetResult();
        }

        public Price InCurrentCurrency(IPricing pricing, IFundDimensionValues fundDimensions) {
            return InCurrentCurrencyAsync(pricing, fundDimensions).GetAwaiter().GetResult();
        }

        public Task<Price> InCurrentCurrencyAsync(IPricing pricing,
                                                  IFundDimensionValues fundDimensions,
                                                  CancellationToken cancellationToken = default) {
            var currentCurrency = _currencyAccessor.GetCurrency();

            return InCurrencyAsync(pricing, fundDimensions, currentCurrency, cancellationToken);
        }

        public Price InCurrency(IPricing pricing, IFundDimensionValues fundDimensions, Currency currency) {
            return InCurrencyAsync(pricing, fundDimensions, currency).GetAwaiter().GetResult();
        }
        
        public async Task<Price> InCurrencyAsync(IPricing pricing,
                                                 IFundDimensionValues fundDimensionValues,
                                                 Currency currency,
                                                 CancellationToken cancellationToken = default) {
            if (!pricing.HasPricing()) {
                return null;
            }
            
            var rules = OrderRules(pricing.Rules);
            var matchedRule = rules.FirstOrDefault(x => RuleMatches(x, fundDimensionValues));

            var amountInBaseCurrency = matchedRule?.Amount ?? pricing.Amount;

            var forexMoney = await _forexConverter.BaseToQuote()
                                                  .ToCurrency(currency)
                                                  .ConvertAsync(amountInBaseCurrency, cancellationToken);

            var price = new Price(forexMoney.Quote.RoundUpToWholeNumber().Amount,
                                  matchedRule?.Locked ?? pricing.Locked);

            return price;
        }

        private IEnumerable<IPricingRule> OrderRules(IEnumerable<IPricingRule> rules) {
            return rules.OrEmpty() 
                        .OrderByDescending(x => new object[] {
                            x.FundDimensions?.Dimension1,
                            x.FundDimensions?.Dimension2,
                            x.FundDimensions?.Dimension3,
                            x.FundDimensions?.Dimension4,
                        }.ExceptNull().Count())
                        .ThenBy(x => x.FundDimensions.Dimension1?.Name)
                        .ThenBy(x => x.FundDimensions.Dimension2?.Name)
                        .ThenBy(x => x.FundDimensions.Dimension3?.Name)
                        .ThenBy(x => x.FundDimensions.Dimension4?.Name);
        }

        private bool RuleMatches(IPricingRule rule, IFundDimensionValues ourValues) {
            if (DimensionMatches(rule.FundDimensions.Dimension1, ourValues.Dimension1) &&
                DimensionMatches(rule.FundDimensions.Dimension2, ourValues.Dimension2) &&
                DimensionMatches(rule.FundDimensions.Dimension3, ourValues.Dimension3) &&
                DimensionMatches(rule.FundDimensions.Dimension4, ourValues.Dimension4)) {
                return true;
            }

            return false;
        }
        
        private bool DimensionMatches<TValue>(TValue ruleValue, TValue ourValue)
            where TValue : FundDimensionValue<TValue> {
            if (!ruleValue.HasValue() || ruleValue == ourValue) {
                return true;
            }
            
            return false;
        }
    }
}