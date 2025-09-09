using Domain.Entities;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    IGenericRepo<TEntity, TKey> GetRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    Task<int> SaveChangesAsync();
}