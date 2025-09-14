using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class GenreService(IUnitOfWork _unitOfWork,IMapper _mapper ):IGenre
{
    public async Task<ResponseGenreDto> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseGenreDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Genre> GetOrCreateAsync(string genre)
    {
        var correctedName= genre.ToLower().Trim();
        var existingGenre = await _unitOfWork.GetRepo<Genre, Guid>().FindByNameAsync(correctedName);
        if(existingGenre is not null)return existingGenre;
        var newGenre =new Genre { Id = Guid.NewGuid(),Name = correctedName };
        await _unitOfWork.GetRepo<Genre, Guid>().AddAsync(newGenre);
        await _unitOfWork.SaveChangesAsync();
        return newGenre;
    }

    public Task<ResponseGenreDto> CreateGenreAsync(CreateOrUpdateGenreDto genreDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateGenreAsync(Guid id, CreateOrUpdateGenreDto genreDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteGenreAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
}