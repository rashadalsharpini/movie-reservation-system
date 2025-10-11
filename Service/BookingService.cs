using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class BookingService:IBookingService
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

    public Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmBookingAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }
}