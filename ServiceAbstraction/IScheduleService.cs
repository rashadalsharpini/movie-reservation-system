using Shared.Dtos;

namespace ServiceAbstraction;

public interface IScheduleService
{
    public Task<ResponseScheduleDto> CreateAsync(CreateScheduleDto dto);
    public Task<bool> UpdateAsync(UpdateScheduleDto dto);
    public Task<bool> DeleteAsync(Guid id);
    
}