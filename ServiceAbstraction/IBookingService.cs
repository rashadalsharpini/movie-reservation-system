using Shared.Dtos;

namespace ServiceAbstraction;

public interface IBookingService
{
    Task<BookingDetailsDto> CreateBookingAsync(int scheduleId, List<int> seatIds,string temporaryId);
    Task<BookingDetailsDto> GetBookingByIdAsync(int bookingId);
    Task<List<BookingHistoryDto>> GetUserBookingsAsync(string userId);
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync(); 
    Task<bool> CancelBookingAsync(Guid bookingId);
   Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds);
    Task ConfirmBookingAsync(Guid bookingId); 
}