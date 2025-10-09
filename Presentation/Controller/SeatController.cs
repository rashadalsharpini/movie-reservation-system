using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos;

namespace Presentation.Controller;

[ApiController]
[Route("/api/[controller]")]
public class SeatController(IServiceManager serviceManager) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseSeatDto>>> GetAllSeatsAsync(int scheduleId)
    {
        var result = await serviceManager.SeatService.GetAllSeatsAsync(scheduleId);
        return Ok(result);
    }

    [HttpPost("CheckSeatAvailability")]
    public async Task<ActionResult<bool>> AreSeatsAvailableAsync(int scheduleId, [FromBody] List<int> seatIds)
    {
        var areAvailable =await serviceManager.SeatService.AreSeatsAvailableAsync(scheduleId, seatIds);
        return Ok(areAvailable);
    }

    [HttpPost("ReserveSeat")]
    public async Task<ActionResult> ReserveSeatAsync(int scheduleId, [FromBody]List<int> seatIds, string temporaryId)
    {
        await serviceManager.SeatService.ReserveSeatAsync(scheduleId, seatIds, temporaryId);
        return Ok(new {message="Seats reserved successfully"});
    }

    [HttpPost]
    public async Task<ActionResult> ReleaseSeatAsync(int scheduleId, [FromBody] List<int> seatIds)
    {
        await serviceManager.SeatService.ReleaseSeatAsync(scheduleId, seatIds);
        return Ok(new {message="Seats released successfully"});
    }
    
}