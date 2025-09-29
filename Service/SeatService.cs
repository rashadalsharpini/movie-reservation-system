using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class SeatService(IUnitOfWork unitOfWork, IMapper mapper):ISeatService
{
    public async Task<IEnumerable<ResponseSeatDto>> GetAllSeatsAsync()
    {
        var seats = await unitOfWork.GetRepo<Seat, int>().GetAllAsync();
        var result = mapper.Map<IEnumerable<ResponseSeatDto>>(seats);
        return result;
    }
}