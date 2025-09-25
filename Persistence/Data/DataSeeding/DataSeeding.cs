using System.Text.Json;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data.DataSeeding;

public class DataSeeding(
    MovieDbContext db,
    ILogger<DataSeeding> logger) : IDataSeeding
{
    public async Task DataSeedAsync()
    {
        try
        {
            if (!db.Database.GetPendingMigrations().Any())
            {
                await db.Database.MigrateAsync();
                // if (!db.Cinema.Any())
                // {
                //     var cinemaData = File.OpenRead(@"Persistence/Data/DataSeeding/Seeds/Cinema.json");
                //     var cinemas = await JsonSerializer.DeserializeAsync<List<Cinema>>(cinemaData);
                //     if (cinemas is not null && cinemas.Any())
                //         await db.Cinema.AddRangeAsync(cinemas);
                //     await db.SaveChangesAsync();
                // }
                if (!db.Genres.Any())
                {
                    var genresData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/Genres.json");
                    var genres = await JsonSerializer.DeserializeAsync<List<Genre>>(genresData);
                    if (genres is not null && genres.Any())
                    {
                        await db.Genres.AddRangeAsync(genres);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Movies.Any())
                {
                    var moviesData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/Movies.json");
                    var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(moviesData);
                    if (movies is not null && movies.Any())
                    {
                        await db.Movies.AddRangeAsync(movies);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Halls.Any())
                {
                    var hallsData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/Halls.json");
                    var halls = await JsonSerializer.DeserializeAsync<List<Hall>>(hallsData);
                    if (halls is not null && halls.Any())
                    {
                        await db.Halls.AddRangeAsync(halls);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Seats.Any())
                {
                    var seatsData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/Seats.json");
                    var seats = await JsonSerializer.DeserializeAsync<List<Seat>>(seatsData);
                    if (seats is not null && seats.Any())
                    {
                        await db.Seats.AddRangeAsync(seats);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.MovieGenres.Any())
                {
                    var movieGenresData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/MovieGenres.json");
                    var movieGenres = await JsonSerializer.DeserializeAsync<List<MovieGenre>>(movieGenresData);
                    if (movieGenres is not null && movieGenres.Any())
                    {
                        await db.MovieGenres.AddRangeAsync(movieGenres);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Schedules.Any())
                {
                    var schedulesData = File.OpenRead(@"../Persistence/Data/DataSeeding/Seeds/Schedules.json");
                    var schedules = await JsonSerializer.DeserializeAsync<List<Schedule>>(schedulesData);
                    if (schedules is not null && schedules.Any())
                    {
                        await db.Schedules.AddRangeAsync(schedules);
                        await db.SaveChangesAsync();
                    }
                }
            }
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