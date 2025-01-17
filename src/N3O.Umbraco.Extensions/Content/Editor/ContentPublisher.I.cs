using Umbraco.Cms.Core.Services;

namespace N3O.Umbraco.Content;

public interface IContentPublisher {
    bool HasProperty(string propertyTypeAlias);
    PublishResult SaveAndPublish();
    OperationResult SaveUnpublished();
    void SetName(string name);
    void Unpublish();

    IContentBuilder Content { get; }
}
