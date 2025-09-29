using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos;

namespace Presentation.Controller;
[ApiController]
[Route("/api/[controller]")]
public class ScheduleController(IServiceManager serviceManager):ApiBaseController
{
    [HttpPost]
    public async Task<ActionResult<ResponseScheduleDto>> CreateAsync(CreateScheduleDto dto)
    {
        var result = await serviceManager.ScheduleService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<bool>> UpdateAsync(int id, UpdateScheduleDto dto)
    {
        var result = await serviceManager.ScheduleService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id)
    {
        var result = await serviceManager.ScheduleService.DeleteAsync(id);
        return Ok(result);
    }
}