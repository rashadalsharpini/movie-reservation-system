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
                var moviesData = File.OpenRead(@"Persistence/Data/DataSeeding/Seeds/Movies.json");
                var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(moviesData);
                if(movies is not null && movies.Any())
                    await db.Movies.AddRangeAsync(movies);
            }

            if (!db.Genres.Any())
            {
                var genresData = File.OpenRead(@"Persistence/Data/DataSeeding/Seeds/Genres.json");
                var genres = await JsonSerializer.DeserializeAsync<List<Genre>>(genresData);
                if (genres is not null && genres.Any())
                    await db.Genres.AddRangeAsync(genres);
            }

            if (!db.Schedules.Any())
            {
                var schedulesData = File.OpenRead(@"Persistence/Data/DataSeeding/Seeds/Schedules.json");
                var schedules = await JsonSerializer.DeserializeAsync<List<Schedule>>(schedulesData);
                if (schedules is not null && schedules.Any())
                    await db.Schedules.AddRangeAsync(schedules);
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