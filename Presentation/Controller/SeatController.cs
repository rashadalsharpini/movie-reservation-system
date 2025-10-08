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

    [HttpPost]
    public async Task<ActionResult> ReserveSeatAsync(int scheduleId, List<int> seatIds, string temporaryId)
    {
        await serviceManager.SeatService.ReserveSeatAsync(scheduleId, seatIds, temporaryId);
        return Ok();
    }
}