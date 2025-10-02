using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared;
using Shared.Dtos;

namespace Presentation.Controller;

public class MovieController(IServiceManager serviceManager, IConfiguration configuration) : ApiBaseController
{
    private readonly HttpClient _httpClient = new();
    private readonly IConfiguration _configuration = configuration;

    [Cache]
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

    [HttpPost]
    public async Task<ActionResult<ResponseMovieScheduleDto>> CreateAsync(CreateMovieDto dto)
    {
        var result = await serviceManager.MovieService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<bool>> UpdateAsync(Guid id, UpdateMovieDto dto)
    {
        var result = await serviceManager.MovieService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> DeleteAsync(Guid id)
    {
        var result = await serviceManager.MovieService.DeleteAsync(id);
        return Ok(result);
    }


    [HttpGet("CreateMovieFromExternal/{title}")]
    public async Task<ActionResult> CreateMovieFromExternal(string title)
    {
        string apikey = _configuration["ApiSettings:OMDBApiKey"]!;
        string url = $"http://www.omdbapi.com/?t={title}&apikey={apikey}";
        var response = await _httpClient.GetStringAsync(url);
        var json = JsonDocument.Parse(response);
        return Ok(await serviceManager.MovieService.CreateMovieFromExternal(json));
    }
}