using N3O.Umbraco.Content;
using N3O.Umbraco.Data.Builders;
using N3O.Umbraco.Data.Models;
using N3O.Umbraco.Data.Parsing;
using N3O.Umbraco.Extensions;
using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Extensions;
using OurDataTypes = N3O.Umbraco.Data.Lookups.DataTypes;
using UmbracoPropertyEditors = Umbraco.Cms.Core.Constants.PropertyEditors;

namespace N3O.Umbraco.Data.Converters;

public class DateTimePropertyConverter : PropertyConverter<LocalDateTime?> {
    public DateTimePropertyConverter(IColumnRangeBuilder columnRangeBuilder) : base(columnRangeBuilder) { }
    
    public override bool IsConverter(UmbracoPropertyInfo propertyInfo) {
        if (!propertyInfo.Type.PropertyEditorAlias.EqualsInvariant(UmbracoPropertyEditors.Aliases.DateTime)) {
            return false;
        }
        
        var configuration = propertyInfo.DataType.ConfigurationAs<DateTimeConfiguration>();

        // h or H in format indicates includes some component of time
        if (!configuration.Format.Contains("h", StringComparison.InvariantCultureIgnoreCase)) {
            return false;
        }

        return true;
    }

    protected override IEnumerable<Cell<LocalDateTime?>> GetCells(IContentProperty contentProperty,
                                                                  UmbracoPropertyInfo propertyInfo) {
        return ExportValue<DateTime?>(contentProperty, x => OurDataTypes.DateTime.Cell(x?.ToLocalDateTime()));
    }

    public override void Import(IContentBuilder contentBuilder,
                                IEnumerable<IPropertyConverter> converters,
                                IParser parser,
                                ErrorLog errorLog,
                                string columnTitlePrefix,
                                UmbracoPropertyInfo propertyInfo,
                                IEnumerable<ImportField> fields) {
        Import(errorLog,
               propertyInfo,
               fields,
               s => parser.DateTime.Parse(s, OurDataTypes.DateTime.GetClrType()),
               (alias, value) => contentBuilder.DateTime(alias).SetDateTime(value));
    }
}
