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
        if (anyConflictSchedule.Any()) throw new Exception("there are a conflict in the sechdule show date time");
        var schedule=mapper.Map<Schedule>(dto);
        await unitOfWork.GetRepo<Schedule, int>().AddAsync(schedule);
        await unitOfWork.SaveChangesAsync();
        return mapper.Map<ResponseScheduleDto>(schedule);
    }

    public async Task<bool> UpdateAsync(int scehduleId, UpdateScheduleDto dto)
    {
        var existingSchedule=await unitOfWork.GetRepo<Schedule,int>().GetByIdAsync(scehduleId);
        if(existingSchedule == null) throw new Exception("Schedule not found");
        existingSchedule = mapper.Map(dto, existingSchedule);
        unitOfWork.GetRepo<Schedule, int>().Update(existingSchedule);
        return await unitOfWork.SaveChangesAsync()>0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingSchedule=await unitOfWork.GetRepo<Schedule,int>().GetByIdAsync(id);
        if(existingSchedule == null) throw new Exception("Schedule not found");
        unitOfWork.GetRepo<Schedule,int>().Delete(existingSchedule);
       return await unitOfWork.SaveChangesAsync()>0;
    }
}