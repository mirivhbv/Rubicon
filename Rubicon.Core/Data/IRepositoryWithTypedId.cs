﻿namespace Rubicon.Core.Data;

public interface IRepositoryWithTypedId<T, TId> where T : IEntityWithTypedId<TId>
{
    IQueryable<T> Query();

    void Add(T entity);

    void AddRange(IEnumerable<T> entity);

    IDbContextTransaction BeginTransaction();

    void SaveChanges();

    Task SaveChangesAsync();

    void Remove(T entity);
}