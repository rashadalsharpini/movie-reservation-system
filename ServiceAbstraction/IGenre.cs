using Domain.Entities;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IGenre
{
    Task<ResponseGenreDto> GetAllAsync();
    Task<ResponseGenreDto> GetByIdAsync(Guid id);
    Task<Genre> GetOrCreateAsync(string  genre);
    Task<ResponseGenreDto> CreateGenreAsync(CreateOrUpdateGenreDto genreDto);
    Task<bool> UpdateGenreAsync(Guid id, CreateOrUpdateGenreDto genreDto);
    Task<bool> DeleteGenreAsync(Guid id);
    
}