using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Contracts;

public interface IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications);
    IQueryable<TEntity> Queryable();
    Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications);
    Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications);
    Task<Genre?> FindByNameAsync(string genreName);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TKey id);
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}