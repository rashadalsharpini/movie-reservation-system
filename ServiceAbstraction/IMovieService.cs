using System.Text.Json;
using Shared;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IMovieService
{
    Task<PaginatedResult<ResponseMovieDto>> GetAllAsync(MovieParameterSpecification parameterSpecification);
    Task<ResponseMovieScheduleDto> GetByIdAsync(Guid id);
    Task<ResponseMovieScheduleDto> CreateAsync(CreateMovieDto dto);
    Task<ResponseMovieScheduleDto> CreateMovieFromExternal(JsonDocument json);
    Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto);
    Task<bool> DeleteAsync(Guid id);
}