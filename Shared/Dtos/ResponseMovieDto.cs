namespace Shared.Dtos;

public class ResponseMovieDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    public int DurationMinutes { get; set; }
    
    public decimal Rating { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    
    public List<string> Genres { get; set; } = null!;
}