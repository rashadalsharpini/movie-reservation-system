using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class MovieDbContext:DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
    }
}