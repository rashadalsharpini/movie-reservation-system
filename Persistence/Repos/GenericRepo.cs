using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repos;

public class GenericRepo<TEntity, TKey>(MovieDbContext db)
    : IGenericRepo<TEntity, TKey> where TEntity : BaseEntity<TKey>
{

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await db.Set<TEntity>().ToListAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(db.Set<TEntity>(), specifications).ToListAsync();

    public IQueryable<TEntity> Queryable()
        => db.Set<TEntity>().AsQueryable();


    public async Task<Genre?> FindByNameAsync(string genreName)
       => await db.Genres.FirstOrDefaultAsync(g=>g.Name.ToLower()==genreName);


    public async Task<TEntity?> GetByIdAsync(TKey id)
        => await db.Set<TEntity>().FindAsync(id);

    public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        => await SpecificationEvaluator.CreateQuery(db.Set<TEntity>(), specifications).FirstOrDefaultAsync();

    public async Task AddAsync(TEntity entity)
        => await db.Set<TEntity>().AddAsync(entity);
    public void Update(TEntity entity)
        => db.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity)
        => db.Set<TEntity>().Remove(entity);
}

