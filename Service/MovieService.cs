using System.Globalization;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Service;

public class MovieService(IUnitOfWork _unitOfWork,IMapper _mapper,IGenre _genre):IMovie
{
    public Task<PaginatedResult<ResponseMovieDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseMovieDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseMovieDto> CreateAsync(CreateMovieDto movieDto)
    {
        var movie = _mapper.Map<Movie>(movieDto);
        foreach (var genreName in movieDto.GenreNames)
        {
            var genre= await _genre.GetOrCreateAsync(genreName);
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
                var genre= await _genre.GetOrCreateAsync(genreName);
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