using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class SeatService(IUnitOfWork unitOfWork):ISeatService
{
    public async Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync(int scheduleId)
    {
        var schedule = await unitOfWork.GetRepo<Schedule, int>().Queryable()
            .Include(s => s.Hall)
            .ThenInclude(h => h.Seats).FirstOrDefaultAsync(s=>s.Id == scheduleId);
       

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