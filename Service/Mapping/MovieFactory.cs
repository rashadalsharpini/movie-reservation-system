using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public static class MovieFactory
{
    public static ResponseMovieScheduleDto ReturnMovieSchedulesToDto(this Movie movie, IEnumerable<Schedule> schedules)
    {
        var movieScheduleDto = new ResponseMovieScheduleDto
        {
            Id = movie.Id,
            Name = movie.Name,
            Description = movie.Description,
            DurationMinutes = movie.DurationMinutes,
            Rating = movie.Rating,
            ReleaseDate = movie.ReleaseDate,
            Genres = movie.Genres.Select(g => g.Name).ToList(),
            Schedules = schedules.ToList()
        };
        return movieScheduleDto;
    }
}