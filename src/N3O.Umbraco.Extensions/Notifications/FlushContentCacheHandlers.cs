using N3O.Umbraco.Content;
using N3O.Umbraco.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace N3O.Umbraco.Notifications {
    public class FlushContentCacheHandlers :
        INotificationAsyncHandler<ContentDeletedNotification>,
        INotificationAsyncHandler<ContentPublishedNotification> {
        private readonly IContentCache _contentCache;

        public FlushContentCacheHandlers(IContentCache contentCache) {
            _contentCache = contentCache;
        }
    
        public async Task HandleAsync(ContentDeletedNotification notification, CancellationToken cancellationToken) {
            await ProcessAsync(notification.DeletedEntities);
        }
        
        public async Task HandleAsync(ContentPublishedNotification notification, CancellationToken cancellationToken) {
            await ProcessAsync(notification.PublishedEntities);
        }

        private Task ProcessAsync(IEnumerable<IContent> entities) {
            var aliases = entities.OrEmpty().Select(x => x.ContentType.Alias).Distinct().ToList();
        
            _contentCache.Flush(aliases);
        
            return Task.CompletedTask;
        }
    }
}