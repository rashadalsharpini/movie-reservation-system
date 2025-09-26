using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<Movie,ResponseMovieDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()));
        CreateMap<Movie, ResponseMovieScheduleDto>()
            .ForMember(m => m.Schedules, m => m.MapFrom(movie => movie.Schedules))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name).ToList()));
        CreateMap<CreateMovieDto, Movie>();
        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(m => m.Genres, opt => opt.Ignore());
    }
}