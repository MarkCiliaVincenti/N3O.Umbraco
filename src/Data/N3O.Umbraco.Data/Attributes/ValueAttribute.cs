using N3O.Umbraco.Data.Lookups;
using System;

namespace N3O.Umbraco.Data.Attributes {
    public abstract class ValueAttribute : ColumnRangeAttribute {
        protected ValueAttribute(int order, DataType dataType) : base(dataType, order) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BoolAttribute : ValueAttribute {
        public BoolAttribute(int order) : base(order, DataTypes.Bool) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DateAttribute : ValueAttribute {
        public DateAttribute(int order) : base(order, DataTypes.Date) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DateTimeAttribute : ValueAttribute {
        public DateTimeAttribute(int order) : base(order, DataTypes.DateTime) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalAttribute : ValueAttribute {
        public DecimalAttribute(int order) : base(order, DataTypes.Decimal) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GuidAttribute : ValueAttribute {
        public GuidAttribute(int order) : base(order, DataTypes.Guid) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IntegerAttribute : ValueAttribute {
        public IntegerAttribute(int order) : base(order, DataTypes.Integer) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupAttribute : ValueAttribute {
        public LookupAttribute(int order) : base(order, DataTypes.Lookup) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MoneyAttribute : ValueAttribute {
        public MoneyAttribute(int order) : base(order, DataTypes.Money) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ReferenceAttribute : ValueAttribute {
        public ReferenceAttribute(int order) : base(order, DataTypes.Reference) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class StringAttribute : ValueAttribute {
        public StringAttribute(int order) : base(order, DataTypes.String) { }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TimeAttribute : ValueAttribute {
        public TimeAttribute(int order) : base(order, DataTypes.Time) { }
    }
    
    [AttributeUsage(AttributeTargets.Property)]
    public class YearMonthAttribute : ValueAttribute {
        public YearMonthAttribute(int order) : base(order, DataTypes.YearMonth) { }
    }
}