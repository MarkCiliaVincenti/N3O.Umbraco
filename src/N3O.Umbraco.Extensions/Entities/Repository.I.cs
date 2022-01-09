﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace N3O.Umbraco.Entities {
    public interface IRepository<T> where T : IEntity {
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}