using N3O.Umbraco.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace N3O.Umbraco.Content {
    public class ContentLocator : IContentLocator {
        private readonly IUmbracoContextFactory _umbracoContextFactory;

        public ContentLocator(IUmbracoContextFactory umbracoContextFactory) {
            _umbracoContextFactory = umbracoContextFactory;
        }

        public IReadOnlyList<IPublishedContent> All(Func<IPublishedContent, bool> predicate = null) {
            return All(null, predicate);
        }

        public IReadOnlyList<IPublishedContent> All(string contentTypeAlias,
                                                        Func<IPublishedContent, bool> predicate = null) {
            var allContent = GetAllContent(contentTypeAlias);
            var filteredContent = allContent.Where(x => predicate?.Invoke(x) ?? true).ToList();

            return filteredContent;
        }

        public IReadOnlyList<T> All<T>(Func<T, bool> predicate = null) {
            if (predicate == null) {
                return All(AliasHelper<T>.ContentTypeAlias()).Select(x => x.As<T>()).ToList();
            } else {
                return All(AliasHelper<T>.ContentTypeAlias(), x => predicate(x.As<T>())).Select(x => x.As<T>()).ToList();
            }
        }

        public IPublishedContent ById(int id) {
            return Run(c => c.Content.GetById(id));
        }

        public T ById<T>(int id) {
            return ById(id).As<T>();
        }

        public IPublishedContent ById(Guid id) {
            return Run(c => c.Content.GetById(id));
        }

        public T ById<T>(Guid id) {
            return ById(id).As<T>();
        }

        public IPublishedContent Single(string contentTypeAlias, Func<IPublishedContent, bool> predicate = null) {
            return All(contentTypeAlias, predicate).SingleOrDefault();
        }

        public T Single<T>(Func<T, bool> predicate = null) {
            if (predicate == null) {
                return Single(AliasHelper<T>.ContentTypeAlias()).As<T>();
            } else {
                return Single(AliasHelper<T>.ContentTypeAlias(), x => predicate(x.As<T>())).As<T>();
            }
        }
    
        private IReadOnlyList<IPublishedContent> GetAllContent(string contentTypeAlias) {
            return Run(c => {
                var allContent = new List<IPublishedContent>();

                foreach (var rootContent in c.Content.GetAtRoot()) {
                    if (contentTypeAlias == null) {
                        allContent.AddRange(rootContent.Descendants());
                    } else {
                        if (rootContent.ContentType.Alias.EqualsInvariant(contentTypeAlias)) {
                            allContent.Add(rootContent);
                        }
                    
                        allContent.AddRange(rootContent.DescendantsOfType(contentTypeAlias));
                    }
                }

                return allContent;
            });
        }

        private T Run<T>(Func<IUmbracoContext, T> func) where T : class {
            using (var contextReference = _umbracoContextFactory.EnsureUmbracoContext()) {
                return func(contextReference.UmbracoContext);
            }
        }
    }
}
