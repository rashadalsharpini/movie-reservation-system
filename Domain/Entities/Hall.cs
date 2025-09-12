using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Hall : BaseEntity<int>
{
    [MaxLength(50)] public string Name { get; set; } = null!;
    public int Capacity { get; set; }
    public Cinema Cinema { get; set; } = null!;
    public Guid CinemaId { get; set; }
}