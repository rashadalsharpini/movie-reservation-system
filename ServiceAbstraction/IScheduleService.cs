using Shared.Dtos;

namespace ServiceAbstraction;

public interface IScheduleService
{
    public Task<ResponseScheduleDto> CreateAsync(CreateScheduleDto dto);
    public Task<bool> UpdateAsync(int scheduleId,UpdateScheduleDto dto);
    public Task<bool> DeleteAsync(int id);
}