using System.Collections.Concurrent;
using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repos;

public class UnitOfWork(MovieDbContext db) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = [];

    public IGenericRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        => (IGenericRepo<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name,
            (_) => new GenericRepo<TEntity, TKey>(db));

    public async Task<int> SaveChangesAsync()
        => await db.SaveChangesAsync();
}