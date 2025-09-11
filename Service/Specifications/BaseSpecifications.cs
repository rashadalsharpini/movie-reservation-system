using System.Linq.Expressions;
using Domain.Contracts;
using Domain.Entities;

namespace Service.Specifications;

public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    public List<Expression<Func<TEntity, object>>>? Includes { get; } = [];
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderBy)
        => OrderBy = orderBy;

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescending)
        => OrderByDescending = orderByDescending;

    protected void AddInclude(Expression<Func<TEntity, object>> include)
        => Includes!.Add(include);
}