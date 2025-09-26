using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Hall : BaseEntity<int>
{
    [JsonIgnore]
    public override int Id { get; set; }
    [MaxLength(50)] public string Name { get; set; } = null!;

    public int Capacity { get; set; }
    
    public Guid CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}