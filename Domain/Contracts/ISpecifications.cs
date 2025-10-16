using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Contracts;

public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>>? Includes { get; }
    public List<Func<IQueryable<TEntity>, IQueryable<TEntity>>>? IncludeExpressions { get; }
    public Expression<Func<TEntity, object>>? OrderBy { get; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    public bool IsPagination { get; set; }
}