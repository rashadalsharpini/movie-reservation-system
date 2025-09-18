using System.Globalization;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Service;

public class MovieService(IUnitOfWork _unitOfWork,IMapper _mapper,IGenreService genreService):IMovieService
{
    public async Task<PaginatedResult<ResponseMovieDto>> GetAllAsync(MovieParameterSpecification parameterSpecification)
    {
        var movies = await _unitOfWork.GetRepo<Movie, Guid>().GetAllAsync(new MovieSpecifications(parameterSpecification));
        var result1= _mapper.Map<IEnumerable<ResponseMovieDto>>(movies);
        var finalResult = new PaginatedResult<ResponseMovieDto>(
            parameterSpecification.PageIndex,
            parameterSpecification.PageSize,
            null,
            result1
        );
        return finalResult;
    }

    public async Task<ResponseMovieDto> GetByIdAsync(Guid id)
    {
        var existingMovie = await _unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(new MovieSpecifications(id));
        return existingMovie !=null ? _mapper.Map<ResponseMovieDto>(existingMovie) : throw new Exception($"the movie with {id}  was not found");
    }

    public async Task<ResponseMovieDto> CreateAsync(CreateMovieDto movieDto)
    {
        var movie = _mapper.Map<Movie>(movieDto);
        foreach (var genreName in movieDto.GenreNames)
        {
            var genre= await genreService.GetOrCreateAsync(genreName);
            movie.Genres.Add(genre);
        }
        await _unitOfWork.GetRepo<Movie, Guid>().AddAsync(movie); 
        await _unitOfWork.SaveChangesAsync();
        return await GetByIdAsync(movie.Id);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto)
    {
        var existingMovie = await _unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(id);
        if (existingMovie == null) throw new Exception($"Movie with id {id} was not found"); 
        existingMovie= _mapper.Map(dto, existingMovie);
        if (dto.GenreNames != null)
        {
            existingMovie.Genres.Clear();
            foreach (var genreName in dto.GenreNames)
            {
                var genre= await genreService.GetOrCreateAsync(genreName);
                existingMovie.Genres.Add(genre);
            }
        }
       _unitOfWork.GetRepo<Movie,Guid>().Update(existingMovie);
       return await _unitOfWork.SaveChangesAsync()>0;
    }
    
    public async Task<bool> DeleteAsync(Guid id)
    {
       var existingMovie= await _unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(id);
       if (existingMovie == null) throw new Exception($"Movie with id {id} was not found");
       _unitOfWork.GetRepo<Movie,Guid>().Delete(existingMovie);
      return await _unitOfWork.SaveChangesAsync()>0;
    }
}