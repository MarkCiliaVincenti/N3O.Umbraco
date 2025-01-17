using Umbraco.Cms.Core.Models;
using static Umbraco.Cms.Core.Constants.PropertyEditors;

namespace N3O.Umbraco.Extensions;

public static class PropertyTypeExtensions {
    public static bool HasEditorAlias(this IPropertyType propertyType, string alias) {
        return propertyType.PropertyEditorAlias.EqualsInvariant(alias);
    }
    
    public static bool IsContentBlocks(this IPropertyType propertyType) {
        return propertyType.HasEditorAlias(Perplex.ContentBlocks.Constants.PropertyEditor.Alias);
    }
    
    public static bool IsDataList(this IPropertyType propertyType) {
        return propertyType.HasEditorAlias("Umbraco.Community.Contentment.DataList");
    }

    public static bool IsNestedContent(this IPropertyType propertyType) {
        return propertyType.HasEditorAlias(Aliases.NestedContent);
    }
    
    public static bool IsPicker(this IPropertyType propertyType) {
        return propertyType.HasEditorAlias(Aliases.MultiNodeTreePicker);
    }
}
