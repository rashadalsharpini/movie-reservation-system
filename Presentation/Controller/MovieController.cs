using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Presentation.Controller;

public class MovieController(IServiceManager serviceManager) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<PaginatedResult<ResponseMovieScheduleDto>>> GetAllMovieSchedules(
        [FromQuery] MovieParameterSpecification parameters)
    {
        var result = await serviceManager.MovieService.GetAllAsync(parameters);
        return Ok(result);
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseMovieScheduleDto>> GetMovieById(Guid id)
    {
        var result = await serviceManager.MovieService.GetByIdAsync(id);
        return Ok(result);
    }

    public async Task<ActionResult<ResponseMovieScheduleDto>> CreateAsync(CreateMovieDto dto)
    {
        var result = await serviceManager.MovieService.CreateAsync(dto);
        return Ok(result);
    }

    public async Task<ActionResult<bool>> UpdateAsync(Guid id, UpdateMovieDto dto)
    {
        var result = await serviceManager.MovieService.UpdateAsync(id, dto);
        return Ok(result);
    }

    public async Task<ActionResult<bool>> DeleteAsync(Guid id)
    {
        var result = await serviceManager.MovieService.DeleteAsync(id);
        return Ok(result);
    }
    
}