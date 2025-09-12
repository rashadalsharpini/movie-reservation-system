using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Movie : BaseEntity<Guid>
{
    [MaxLength(100)] public string Name { get; set; } = null!;

    [MaxLength(1000)] public string Description { get; set; } = null!;
    public int DurationMinutes { get; set; }
    public decimal Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<Genre> Genres { get; set; } = [];
}