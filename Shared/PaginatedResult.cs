using Shared.Dtos;

namespace Shared;

public record PaginatedResult<TEntity>(int PageIndex, int PageSize, int? TotalCount, IEnumerable<ResponseMovieDto> Data);