using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class BookingService(IUnitOfWork unitOfWork):IBookingService
{
    public Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<BookingDetailsDto> GetBookingByIdAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }

    public Task<List<BookingHistoryDto>> GetUserBookingsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<List<BookingDto>> GetAllBookingsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CancelBookingAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }

    public async Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds)
    {
        var schedulePrice =await unitOfWork.GetRepo<Schedule, int>().GetByIdAsync(scheduleId);
        if(schedulePrice == null) throw new Exception("Schedule id is not found");
        return schedulePrice.BasePrice*seatIds.Count;
    }

    public Task ConfirmBookingAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }
}