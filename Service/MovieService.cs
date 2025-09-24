using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Service;

public class MovieService(IUnitOfWork unitOfWork, IMapper mapper, IGenreService genreService) : IMovieService
{
    public async Task<PaginatedResult<ResponseMovieScheduleDto>> GetAllAsync(
        MovieParameterSpecification parameterSpecification = null!)
    {
        var movies = await unitOfWork.GetRepo<Movie, Guid>()
            .GetAllAsync(new MovieSpecifications(parameterSpecification));
        var result1 = mapper.Map<IEnumerable<ResponseMovieScheduleDto>>(movies);
        var finalResult = new PaginatedResult<ResponseMovieScheduleDto>(
            parameterSpecification.PageIndex,
            parameterSpecification.PageSize,
            null,
            result1
        );
        return finalResult;
    }

    public async Task<ResponseMovieScheduleDto> GetByIdAsync(Guid id)
    {
        var existingMovie = await unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(new MovieSpecifications(id));
        return mapper.Map<ResponseMovieScheduleDto>(existingMovie);
    }

    public async Task<ResponseMovieScheduleDto> CreateAsync(CreateMovieDto movieDto)
    {
        var movie = mapper.Map<Movie>(movieDto);
        foreach (var genreName in movieDto.GenreNames)
        {
            var genre = await genreService.GetOrCreateAsync(genreName);
            movie.Genres.Add(genre);
        }

        await unitOfWork.GetRepo<Movie, Guid>().AddAsync(movie);
        await unitOfWork.SaveChangesAsync();
        return await GetByIdAsync(movie.Id);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto)
    {
        var existingMovie = await unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(id);
        if (existingMovie == null) throw new Exception($"Movie with id {id} was not found");
        existingMovie = mapper.Map(dto, existingMovie);
        if (dto.GenreNames != null)
        {
            existingMovie.Genres.Clear();
            foreach (var genreName in dto.GenreNames)
            {
                var genre = await genreService.GetOrCreateAsync(genreName);
                existingMovie.Genres.Add(genre);
            }
        }

        unitOfWork.GetRepo<Movie, Guid>().Update(existingMovie);
        return await unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingMovie = await unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(id);
        if (existingMovie == null) throw new Exception($"Movie with id {id} was not found");
        unitOfWork.GetRepo<Movie, Guid>().Delete(existingMovie);
        return await unitOfWork.SaveChangesAsync() > 0;
    }
}