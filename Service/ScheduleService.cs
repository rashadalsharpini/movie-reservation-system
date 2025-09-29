using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using ServiceAbstraction;
using Shared.Dtos;

namespace Service;

public class ScheduleService(IUnitOfWork unitOfWork,IMapper mapper):IScheduleService
{
    public async Task<ResponseScheduleDto> CreateAsync(CreateScheduleDto dto)
    {
        var existingMovie = await unitOfWork.GetRepo<Movie, Guid>().GetByIdAsync(dto.MovieId);
        if(existingMovie == null)throw new Exception("Movie not found");
        var endTime = dto.ShowDateTime.AddMinutes(existingMovie.DurationMinutes);
        var anyConflictSchedule = await unitOfWork.GetRepo<Schedule, int>().FindAllAsync(s => s.HallId == dto.HallId && 
            s.ShowDateTime < endTime &&
            s.ShowDateTime.AddMinutes(s.Movie.DurationMinutes) > dto.ShowDateTime);
        if (anyConflictSchedule.Any()) throw new Exception("there are a conflict schedule");
        var schedule=mapper.Map<Schedule>(dto);
        await unitOfWork.GetRepo<Schedule, int>().AddAsync(schedule);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<ResponseScheduleDto>(schedule);
    }

    public Task<bool> UpdateAsync(UpdateScheduleDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}