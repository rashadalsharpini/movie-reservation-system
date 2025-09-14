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

    public Task<bool> UpdateAsync(Guid id, UpdateMovieDto dto)
    {
        throw new NotImplementedException();
    }
    
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}