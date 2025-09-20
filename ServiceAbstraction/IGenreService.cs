using Domain.Entities;
using Shared;
using Shared.Dtos;

namespace ServiceAbstraction;

public interface IGenreService
{
    Task<Genre> GetOrCreateAsync(string genre);
}