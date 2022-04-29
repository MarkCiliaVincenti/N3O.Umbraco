using N3O.Umbraco.Data.Lookups;
using N3O.Umbraco.Localization;
using N3O.Umbraco.Security;
using System;
using System.Collections.Generic;

namespace N3O.Umbraco.Data.Models {
    public class Column : Value {
        public Column(DataType dataType,
                      string title,
                      string comment,
                      IFormatter formatter,
                      ILocalClock localClock,
                      LocalizationSettings localizationSettings,
                      bool hidden,
                      AccessControlList accessControlList,
                      IEnumerable<Attribute> attributes,
                      IReadOnlyDictionary<string, IReadOnlyList<object>> metadata) {
            DataType = dataType;
            Title = title;
            Comment = comment;
            Formatter = formatter;
            LocalClock = localClock;
            LocalizationSettings = localizationSettings;
            Hidden = hidden;
            AccessControlList = accessControlList;
            Attributes = attributes;
            Metadata = metadata;
        }

        public DataType DataType { get; }
        public string Title { get; }
        public string Comment { get; }
        public bool Hidden { get; }
        public AccessControlList AccessControlList { get; }
        public IEnumerable<Attribute> Attributes { get; }
        public IFormatter Formatter { get; }
        public ILocalClock LocalClock { get; }
        public LocalizationSettings LocalizationSettings { get; }
        public IReadOnlyDictionary<string, IReadOnlyList<object>> Metadata { get; }
    }
}