using N3O.Umbraco.Content;
using N3O.Umbraco.Data.Lookups;
using N3O.Umbraco.Data.Models;
using N3O.Umbraco.Data.Parsing;
using N3O.Umbraco.Extensions;
using NodaTime;
using NodaTime.Extensions;
using System;
using System.Collections.Generic;
using UmbracoPropertyEditors = Umbraco.Cms.Core.Constants.PropertyEditors;

namespace N3O.Umbraco.Data.Converters {
    public class DateTimePropertyConverter : PropertyConverter {
        public override bool IsConverter(UmbracoPropertyInfo propertyInfo) {
            return propertyInfo.Type.PropertyEditorAlias.EqualsInvariant(UmbracoPropertyEditors.Aliases.DateTime);
        }

        public override IEnumerable<Cell> Export(ContentProperties content, UmbracoPropertyInfo propertyInfo) {
            return ExportValue<DateTime?>(content,
                                          propertyInfo,
                                          x => x == null ? null : DataTypes.DateTime.Cell(x.Value.ToLocalDateTime()));
        }

        public override void Import(IContentBuilder contentBuilder,
                                    IParser parser,
                                    UmbracoPropertyInfo propertyInfo,
                                    IEnumerable<string> values) {
            Import(propertyInfo,
                   values,
                   s => parser.DateTime.Parse(s, typeof(LocalDateTime?)),
                   (alias, value) => contentBuilder.DateTime(alias).SetDateTime(value));
        }
    }
}