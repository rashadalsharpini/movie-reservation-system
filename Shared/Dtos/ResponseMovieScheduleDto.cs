namespace Shared.Dtos;

public class ResponseMovieScheduleDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public int DurationMinutes { get; set; }
    
    public decimal Rating { get; set; }
    
    public DateOnly ReleaseDate { get; set; }
    
    public List<string> Genres { get; set; } = null!;
    public ICollection<ScheduleDto> Schedules { get; set; } = new List<ScheduleDto>();
}