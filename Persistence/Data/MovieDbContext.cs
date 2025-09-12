using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class MovieDbContext:DbContext
{
    public MovieDbContext(DbContextOptions<MovieDbContext> options):base(options){}

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Cinema> Cinema { get; set; }
    public DbSet<Hall>  Halls { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDbContext).Assembly);
    }
}