using Domain.Entities;

namespace Domain.Contracts;

public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
    IQueryable<TEntity> Queryable();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}