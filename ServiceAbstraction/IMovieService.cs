using Shared;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IMovieService
{
    Task<PaginatedResult<ResponseMovieScheduleDto>> GetAllAsync(MovieParameterSpecification parameterSpecification);
    Task<ResponseMovieScheduleDto> GetByIdAsync(Guid id);
    Task<ResponseMovieScheduleDto> CreateAsync(CreateMovieDto dto);
    Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto);
    Task<bool> DeleteAsync(Guid id);
}