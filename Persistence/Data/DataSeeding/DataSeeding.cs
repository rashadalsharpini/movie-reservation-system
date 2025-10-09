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
            var solutionDir = Path.Combine(AppContext.BaseDirectory, @"../../../..");
            if (!(await db.Database.GetPendingMigrationsAsync()).Any())
            {
                await db.Database.MigrateAsync();
                if (!db.Cinema.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Cinema.json");
                    var cinemaData = File.OpenRead(data);
                    var cinemas = await JsonSerializer.DeserializeAsync<List<Cinema>>(cinemaData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (cinemas is not null && cinemas.Any())
                        await db.Cinema.AddRangeAsync(cinemas);
                    await db.SaveChangesAsync();
                }

                if (!db.Genres.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Genres.json");
                    var genresData = File.OpenRead(data);
                    var genres = await JsonSerializer.DeserializeAsync<List<Genre>>(genresData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (genres is not null && genres.Any())
                    {
                        await db.Genres.AddRangeAsync(genres);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Movies.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Movies.json");
                    var moviesData = File.OpenRead(data);
                    var movies = await JsonSerializer.DeserializeAsync<List<Movie>>(moviesData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (movies is not null && movies.Any())
                    {
                        await db.Movies.AddRangeAsync(movies);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Halls.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Halls.json");
                    var hallsData = File.OpenRead(data);
                    var halls = await JsonSerializer.DeserializeAsync<List<Hall>>(hallsData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (halls is not null && halls.Any())
                    {
                        await db.Halls.AddRangeAsync(halls);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Seats.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Seats.json");
                    var seatsData = File.OpenRead(data);
                    var seats = await JsonSerializer.DeserializeAsync<List<Seat>>(seatsData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (seats is not null && seats.Any())
                    {
                        await db.Seats.AddRangeAsync(seats);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Schedules.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Schedules.json");
                    var schedulesData = File.OpenRead(data);
                    var schedules = await JsonSerializer.DeserializeAsync<List<Schedule>>(schedulesData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (schedules is not null && schedules.Any())
                    {
                        await db.Schedules.AddRangeAsync(schedules);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Bookings.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Booking.json");
                    var bookingData = File.OpenRead(data);
                    var booking = await JsonSerializer.DeserializeAsync<List<Booking>>(bookingData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (booking is not null && booking.Any())
                    {
                        await db.Bookings.AddRangeAsync(booking);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.SeatReservations.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/SeatReservation.json");
                    var seatReservationData = File.OpenRead(data);
                    var seatReservation = await JsonSerializer.DeserializeAsync<List<SeatReservation>>(
                        seatReservationData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (seatReservation is not null && seatReservation.Any())
                    {
                        await db.SeatReservations.AddRangeAsync(seatReservation);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.Tickets.Any())
                {
                    var data = Path.Combine(solutionDir, "Persistence/Data/DataSeeding/Seeds/Ticket.json");
                    var ticketData = File.OpenRead(data);
                    var ticket = await JsonSerializer.DeserializeAsync<List<Ticket>>(ticketData,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (ticket is not null && ticket.Any())
                    {
                        await db.Tickets.AddRangeAsync(ticket);
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