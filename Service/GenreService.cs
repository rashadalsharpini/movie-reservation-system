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

    public async Task<bool> UpdateGenreAsync(Guid id, CreateOrUpdateGenreDto genreDto)
    {
        var existingGenre = await _unitOfWork.GetRepo<Genre, Guid>().GetByIdAsync(id);
        if (existingGenre == null) throw new Exception($"Genre with id {id} was not found"); 
        existingGenre= _mapper.Map(genreDto, existingGenre);
        _unitOfWork.GetRepo<Genre,Guid>().Update(existingGenre);
        return await _unitOfWork.SaveChangesAsync()>0;
    }

    public async Task<bool> DeleteGenreAsync(Guid id)
    {
        var existingGenre= await _unitOfWork.GetRepo<Genre, Guid>().GetByIdAsync(id);
        if (existingGenre == null) throw new Exception($"Genre with id {id} was not found");
        _unitOfWork.GetRepo<Genre,Guid>().Delete(existingGenre);
        return await _unitOfWork.SaveChangesAsync()>0;
    }
    
}