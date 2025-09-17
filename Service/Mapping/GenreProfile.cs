using AutoMapper;
using Domain.Entities;
using Shared.Dtos;

namespace Service.Mapping;

public class GenreProfile:Profile
{
    public GenreProfile()
    {
        CreateMap<CreateOrUpdateGenreDto, Genre>();
        CreateMap<Genre,ResponseGenreDto>();
    }
}