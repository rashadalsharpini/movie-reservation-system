using Domain.Entities;

namespace Domain.Contracts;

public interface IScheduleRepo
{

    public Task<IEnumerable<Schedule>> GetSchedulesByMovieIdAsync(Guid movieId);
}