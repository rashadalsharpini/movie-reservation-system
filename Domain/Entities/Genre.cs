using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Genre : BaseEntity<Guid>
{
    [MaxLength(50)] public string Name { get; set; } = null!;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}