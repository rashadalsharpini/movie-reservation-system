using Domain.Entities;

namespace Domain.Contracts;

public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    IQueryable<TEntity> Queryable();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}