using Shared.Dtos;

namespace ServiceAbstraction;

public interface ISeatService
{
    Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync();
}