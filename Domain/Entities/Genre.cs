namespace Domain.Entities;

public class Genre:BaseEntity<Guid>
{
    public string Name { get; set; }
    public ICollection<Movie> Movies { get; set; }
}