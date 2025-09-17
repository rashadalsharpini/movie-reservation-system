using Shared;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IMovie
{
    Task<PaginatedResult<ResponseMovieDto>> GetAllAsync(MovieParameterSpecification parameterSpecification);
    Task<ResponseMovieDto> GetByIdAsync(Guid id);
    Task<ResponseMovieDto> CreateAsync(CreateMovieDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto);
    Task<bool> DeleteAsync(Guid id);
}