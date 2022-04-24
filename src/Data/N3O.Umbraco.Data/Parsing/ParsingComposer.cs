﻿using Microsoft.Extensions.DependencyInjection;
using N3O.Umbraco.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace N3O.Umbraco.Data.Parsing {
    public class ParsingComposer : Composer {
        public override void Compose(IUmbracoBuilder builder) {
            builder.Services.AddSingleton<IBoolParser, BoolParser>();
            builder.Services.AddSingleton<IDateParserFactory, DateParserFactory>();
            builder.Services.AddSingleton<IDecimalParserFactory, DecimalParserFactory>();
            builder.Services.AddSingleton<IGuidParser, GuidParser>();
            builder.Services.AddSingleton<IIntegerParserFactory, IntegerParserFactory>();
            builder.Services.AddSingleton<ILookupParser, LookupParser>();
            builder.Services.AddSingleton<IParserFactory, ParserFactory>();
            builder.Services.AddSingleton<IReferenceParser, ReferenceParser>();
            builder.Services.AddSingleton<IStringParser, StringParser>();
            builder.Services.AddSingleton<ITimeParser, TimeParser>();
            builder.Services.AddSingleton<IParserFactory, ParserFactory>();
            builder.Services.AddSingleton<IYearMonthParserFactory, YearMonthParserFactory>();
        }
    }
}