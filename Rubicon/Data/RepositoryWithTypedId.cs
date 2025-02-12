﻿namespace Rubicon.Data;

public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
{
    /// <summary>
    /// 
    /// </summary>
    public RepositoryWithTypedId(AppDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }

    protected DbContext Context { get; }

    protected DbSet<T> DbSet { get; }


    public IQueryable<T> Query()
    {
        return DbSet;
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entity)
    {
        DbSet.AddRange(entity);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return Context.Database.BeginTransaction();
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }
}