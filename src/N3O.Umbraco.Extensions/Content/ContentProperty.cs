using System.Collections.Generic;
using Umbraco.Cms.Core.Models;

namespace N3O.Umbraco.Content {
    public interface IContentProperty {
        IContentType ContentType { get; }
        IPropertyType Type { get; }
        string Alias => Type.Alias;
        string Name => Type.Name;
        object Value { get; }
    }
    
    public class ContentProperty<T> : IContentProperty {
        protected ContentProperty(IContentType contentType, IPropertyType type, T value) {
            ContentType = contentType;
            Type = type;
            Value = value;
        }

        public IContentType ContentType { get; }
        public IPropertyType Type { get; }
        public string Alias => Type.Alias;
        public string Name => Type.Name;
        public T Value { get; }

        object IContentProperty.Value => Value;
    }

    public class ContentProperty : ContentProperty<object> {
        public ContentProperty(IContentType contentType, IPropertyType type, object value)
            : base(contentType, type, value) { }
    }

    public class NestedContentProperty : ContentProperty<IReadOnlyList<ContentProperties>> {
        public NestedContentProperty(IContentType contentType,
                                     IPropertyType type,
                                     IReadOnlyList<ContentProperties> value,
                                     string json)
            : base(contentType, type, value) {
            Json = json;
        }
        
        public string Json { get; }
    }
}