using Domain.Entities;
using Shared;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IGenreService
{
    Task<PaginatedResult<ResponseGenreDto>> GetAllAsync(MovieParameterSpecification parameterSpecification);
    Task<Genre> GetOrCreateAsync(string genre);
    Task<ResponseGenreDto> CreateGenreAsync(CreateOrUpdateGenreDto genreDto);
    Task<bool> UpdateGenreAsync(Guid id, CreateOrUpdateGenreDto genreDto);
    Task<bool> DeleteGenreAsync(Guid id);
}