using Domain.Entities;

namespace ServiceAbstraction;

public interface IGenreService
{
    Task<Genre> GetOrCreateAsync(string genre);
}