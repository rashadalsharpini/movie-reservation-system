using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class SeatService(IUnitOfWork unitOfWork) : ISeatService
{
    public async Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync(int scheduleId)
    {
        var schedule = await unitOfWork.GetRepo<Schedule, int>().Queryable()
            .Include(s => s.Hall)
            .ThenInclude(h => h.Seats).FirstOrDefaultAsync(s => s.Id == scheduleId);
        var bookedSeats = await unitOfWork.GetRepo<Ticket, Guid>().Queryable()
            .Where(t => t.ScheduleId == scheduleId)
            .Select(t => t.SeatId).ToListAsync();
        var reservedSeats = await unitOfWork.GetRepo<SeatReservation, Guid>().Queryable()
            .Where(sr => sr.ScheduleId == scheduleId)
            .Select(sr => sr.SeatId).ToListAsync();
        var result = schedule.Hall.Seats.Select(s => new ResponseSeatDto()
        {
            SeatId = s.Id,
            SeatNumber = s.SeatNumber,
            SeatRow = s.SeatRow,
            Status = bookedSeats.Contains(s.Id) ? "Booked" :
                reservedSeats.Contains(s.Id) ? "Reserved" : "Available"
        }).ToList();

        return result;
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