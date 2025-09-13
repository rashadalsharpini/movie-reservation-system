using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Cinema : BaseEntity<Guid>
{
    [MaxLength(50)] public string Name { get; set; } = null!;
    [MaxLength(100)] public string Location { get; set; } = null!;
    
    public ICollection<Hall> Halls { get; set; } = new List<Hall>();
}