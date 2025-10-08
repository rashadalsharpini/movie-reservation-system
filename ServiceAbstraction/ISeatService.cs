using Shared.Dtos;

namespace ServiceAbstraction;

public interface ISeatService
{
    Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync(int scheduleId);
    Task<bool> AreSeatsAvailableAsync(int scheduleId,List<int> seatIds);
    Task ReserveSeatAsync(int scheduleId, List<int> seatIds,string temporaryId);
    Task ReleaseSeatAsync(int scheduleId, List<int> seatIds);
    Task ReleaseExpiredSeatsAsync();
}