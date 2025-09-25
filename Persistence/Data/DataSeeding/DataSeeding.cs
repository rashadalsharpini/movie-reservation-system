using System.Text.Json;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data.DataSeeding;

public class DataSeeding(
    MovieDbContext db,
    ILogger<DataSeeding> logger):IDataSeeding
{
    public async Task DataSeedAsync()
    {
        try
        {
            var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await db.Database.MigrateAsync();
            if (!db.Movies.Any())
            {
                var moviesData = File.OpenRead(@"Persistence/Data/DataSeeding/Seeds/movies.json");
                var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(moviesData);
                if(movies is not null && movies.Any())
                    await db.Movies.AddRangeAsync(movies);
            }

            await db.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    public Task IdentitySeedAsync()
    {
        throw new NotImplementedException();
    }
}