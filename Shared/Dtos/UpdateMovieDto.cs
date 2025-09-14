using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos;

public class UpdateMovieDto
{
    [MaxLength(100)] public string? Name { get; set; }

    [MaxLength(1000)] public string? Description { get; set; }

    [Range(1, 300)] public int? DurationMinutes { get; set; }

    [Range(0, 10)] public decimal? Rating { get; set; }

    [DataType(DataType.Date)] public DateTime? ReleaseDate { get; set; }

    public List<string>? GenreNames { get; set; } = new List<string>();
}