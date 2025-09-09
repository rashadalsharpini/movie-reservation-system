using Domain.Contracts;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Repos;

public class UnitOfWork(MovieDbContext db):IUnitOfWork
{
    private readonly Dictionary<string, object> _repos = [];
    public IGenericRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var type = typeof(TEntity).Name;
        if (_repos.TryGetValue(type, out object? value))
            return (IGenericRepo<TEntity, TKey>)value;
        var repo = new GenericRepo<TEntity, TKey>(db);
        _repos[type] = repo;
        return repo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await db.SaveChangesAsync();
    }
}