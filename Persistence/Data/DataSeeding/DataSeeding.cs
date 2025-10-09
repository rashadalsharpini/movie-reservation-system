using System.Text.Json;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data.DataSeeding;

public class DataSeeding(
    MovieDbContext db,
    MovieIdentityDbContext identityDb,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    ILogger<DataSeeding> logger) : IDataSeeding
{
    public async Task DataSeedAsync()
    {
        try
        {
            var solutionDir = Path.Combine(AppContext.BaseDirectory, @"../../../..");
            if ((await db.Database.GetPendingMigrationsAsync()).Any())
                await db.Database.MigrateAsync();

            if (!db.Cinema.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Cinema.json");
                var cinemaData = File.OpenRead(data);
                var cinemas = await JsonSerializer.DeserializeAsync<List<Cinema>>(cinemaData);
                if (cinemas is not null && cinemas.Any())
                    await db.Cinema.AddRangeAsync(cinemas);
            }

            if (!db.Genres.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Genres.json");
                var genresData = File.OpenRead(data);
                var genres = await JsonSerializer.DeserializeAsync<List<Genre>>(genresData);
                if (genres is not null && genres.Any())
                    await db.Genres.AddRangeAsync(genres);
            }

            if (!db.Movies.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Movies.json");
                var moviesData = File.OpenRead(data);
                var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(moviesData);
                if (movies is not null && movies.Any())
                {
                    await db.Movies.AddRangeAsync(movies);
                }
            }

            if (!db.Halls.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Halls.json");
                var hallsData = File.OpenRead(data);
                var halls = await JsonSerializer.DeserializeAsync<List<Hall>>(hallsData);
                if (halls is not null && halls.Any())
                    await db.Halls.AddRangeAsync(halls);
            }

            if (!db.Seats.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Seats.json");
                var seatsData = File.OpenRead(data);
                var seats = await JsonSerializer.DeserializeAsync<List<Seat>>(seatsData);
                if (seats is not null && seats.Any())
                    await db.Seats.AddRangeAsync(seats);
            }

            if (!db.Schedules.Any())
            {
                var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Schedules.json");
                var schedulesData = File.OpenRead(data);
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


    public async Task IdentityDataSeedAsync()
    {
        try
        {
            var pendingMigrations = await identityDb.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await identityDb.Database.MigrateAsync();
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
                await roleManager.CreateAsync(new IdentityRole("Supervisor"));
            }

            if (!userManager.Users.Any())
            {
                var user01 = new User()
                {
                    Email = "rashad@gmail.com",
                    DisplayName = "rashad",
                    UserName = "rashad",
                    PhoneNumber = "1234567890"
                };
                var user02 = new User()
                {
                    Email = "sayed@gmail.com",
                    DisplayName = "sayed",
                    UserName = "sayed",
                    PhoneNumber = "1234567890"
                };
                await userManager.CreateAsync(user01, "P@ssw0rd");
                await userManager.CreateAsync(user02, "P@ssw0rd");
                await userManager.AddToRoleAsync(user01, "Admin");
                await userManager.AddToRoleAsync(user02, "Supervisor");
            }

            await identityDb.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            Console.WriteLine(e);
            throw;
        }
    }
}