using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class CreateMovieDto
{
    [Required] [MaxLength(100)] public string Name { get; set; } = null!;
    
    [Required] [MaxLength(1000)] public string Description { get; set; } = null!;
    
    [Range(1, 300)] public int DurationMinutes { get; set; }
    
    [Range(0, 10)] public decimal Rating { get; set; }
    
    [DataType(DataType.Date)] public DateTime ReleaseDate { get; set; }
    
    public List<string> GenreNames { get; set; } = new List<string>();
}