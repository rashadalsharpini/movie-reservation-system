using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

public class Movie : BaseEntity<Guid>
{
    public Movie(){}
    [MaxLength(100)] public string Name { get; set; } = null!;

    [MaxLength(1000)] public string Description { get; set; } = null!;

    public int DurationMinutes { get; set; }
    [Precision(5,2)]
    public decimal Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}