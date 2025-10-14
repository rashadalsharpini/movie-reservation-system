using Shared.Dtos;

namespace ServiceAbstraction;

public interface IBookingService
{
    Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto);
    Task<BookingDetailsDto> GetBookingByIdAsync(Guid bookingId);
    Task<List<BookingHistoryDto>> GetUserBookingsAsync(string userId);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync(); 
    Task<bool> CancelBookingAsync(Guid bookingId);
    Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds);
    Task ConfirmBookingAsync(Guid bookingId); 
}