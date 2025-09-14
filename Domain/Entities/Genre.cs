using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Genre : BaseEntity<Guid>
{
    public Genre()
    {
        Id = Guid.NewGuid();
    }
    [MaxLength(50)] public string Name { get; set; } = null!;
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}