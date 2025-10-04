using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class SeatService(IUnitOfWork unitOfWork, IMapper mapper):ISeatService
{
    public Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync(int scheduleId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AreSeatsAvailableAsync(int scheduleId, List<int> seatIds)
    {
        throw new NotImplementedException();
    }

    public Task ReserveSeatAsync(int scheduleId, List<int> seatIds)
    {
        throw new NotImplementedException();
    }

    public Task ReleaseSeatAsync(int scheduleId, List<int> seatIds)
    {
        throw new NotImplementedException();
    }

    public Task ReleaseExpiredSeatsAsync()
    {
        throw new NotImplementedException();
    }
}