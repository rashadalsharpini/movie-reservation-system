using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public class MovieProfile: Profile
{
    public MovieProfile()
    {
        CreateMap<Movie, ResponseMovieScheduleDto>().ForMember(m=>m.Schedules, m=>m.MapFrom(movie=>movie.Schedules));
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(m=>m.Genres,opt=>opt.Ignore());
    }
}