using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;

namespace Service;

public class GenreService(IUnitOfWork _unitOfWork,IMapper _mapper ): IGenreService
{
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
}