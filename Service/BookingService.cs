using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Service.Specifications;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class BookingService(IUnitOfWork unitOfWork, IMapper mapper) : IBookingService
{
    public Task<BookingResponseDto> CreateBookingAsync(CreateBookingDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<BookingDetailsDto> GetBookingByIdAsync(int bookingId)
    {
        var booking = await unitOfWork.GetRepo<Booking, int>().GetByIdAsync(new BookingSpecifications(bookingId));
        var result= mapper.Map<BookingDetailsDto>(booking);
        return result;
    }

    public Task<List<BookingHistoryDto>> GetUserBookingsAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        // ** Ya Rashade **
        //this method is not completed yest because it need to use specification and also pagination and don't forget to upload includs with bookings
        // and you can make the includs here in the service but put it in specification 
        var bookings = await unitOfWork.GetRepo<Booking, int>().GetAllAsync();
        var result = mapper.Map<IEnumerable<BookingDto>>(bookings);
        return result;
    }

    public Task<bool> CancelBookingAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }

    public async Task<decimal> CalculateTotalPriceAsync(int scheduleId, List<int> seatIds)
    {
        var schedulePrice = await unitOfWork.GetRepo<Schedule, int>().GetByIdAsync(scheduleId);
        if (schedulePrice == null) throw new Exception("Schedule id is not found");
        return schedulePrice.BasePrice * seatIds.Count;
    }

    public Task ConfirmBookingAsync(Guid bookingId)
    {
        throw new NotImplementedException();
    }
}