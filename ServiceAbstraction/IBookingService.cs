using Shared.Dtos;

namespace ServiceAbstraction;

public interface IBookingService
{
    Task<BookingDetailsDto> CreateBookingAsync(int scheduleId, List<int> seatIds,string temporaryId,string userId);
    Task<BookingDetailsDto> GetBookingByIdAsync(int bookingId);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync(); 
    Task<bool> CancelBookingAsync(int bookingId);
   Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds);
    Task ConfirmBookingAsync(int bookingId); 
}