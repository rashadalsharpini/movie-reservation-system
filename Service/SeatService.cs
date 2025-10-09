using System.Runtime.CompilerServices;
using AutoMapper;
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

    public async Task<bool> AreSeatsAvailableAsync(int scheduleId, List<int> seatIds)
    {
        var seatReservationCheck = await unitOfWork.GetRepo<SeatReservation, Guid>().Queryable()
            .AnyAsync(sr =>
                sr.ScheduleId == scheduleId && seatIds.Contains(sr.SeatId) && sr.ExpirationDate > DateTime.UtcNow);
        if (seatReservationCheck) return false;
        var seatTicketCheck = await unitOfWork.GetRepo<Ticket, Guid>().Queryable()
            .AnyAsync(t =>
                t.ScheduleId == scheduleId && seatIds.Contains(t.SeatId) &&
                t.Booking.Status != BookingStatus.Cancelled);
        if (seatTicketCheck) return false;
        return true;
    }

    public async Task ReserveSeatAsync(int scheduleId, List<int> seatIds, string temporaryId)
    {
        var availableSeats = await AreSeatsAvailableAsync(scheduleId, seatIds);
        if (!availableSeats) throw new Exception("Seats are not available");
        foreach (var seat in seatIds)
        {
            var newReservationSeat = new SeatReservation()
            {
                TemporaryId = temporaryId,
                SeatId = seat,
                ScheduleId = scheduleId,
                ExpirationDate = DateTime.UtcNow.AddMinutes(5),
                ReservationDate = DateTime.UtcNow
            };
            await unitOfWork.GetRepo<SeatReservation, Guid>().AddAsync(newReservationSeat);
            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task ReleaseSeatAsync(int scheduleId, List<int> seatIds)
    {
        if (!seatIds.Any()) return;
        var deletedReservationSeats = await unitOfWork.GetRepo<SeatReservation, Guid>().Queryable()
            .Where(sr => sr.ScheduleId == scheduleId && seatIds.Contains(sr.SeatId)).ToListAsync();
        if (!deletedReservationSeats.Any()) return;
        foreach (var seat in deletedReservationSeats) unitOfWork.GetRepo<SeatReservation, Guid>().Delete(seat);
        await unitOfWork.SaveChangesAsync();
    }
    
}