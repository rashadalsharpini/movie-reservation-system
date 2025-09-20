using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repos;

public class ScheduleRepo(MovieDbContext db) :IScheduleRepo
{
    public async Task<IEnumerable<Schedule>> GetSchedulesByMovieIdAsync(Guid movieId)
        => await db.Schedules.Where(s => s.MovieId == movieId).ToListAsync();
}