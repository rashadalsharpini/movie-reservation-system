using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;

namespace Service;

public class GenreService(IUnitOfWork unitOfWork ): IGenreService
{
    public async Task<Genre> GetOrCreateAsync(string genre)
    {
        var correctedName= genre.ToLower().Trim();
        var existingGenre = await unitOfWork.GetRepo<Genre, Guid>().FindByNameAsync(correctedName);
        if(existingGenre is not null)return existingGenre;
        var newGenre =new Genre { Id = Guid.NewGuid(),Name = correctedName };
        await unitOfWork.GetRepo<Genre, Guid>().AddAsync(newGenre);
        await unitOfWork.SaveChangesAsync();
        return newGenre;
    }
}