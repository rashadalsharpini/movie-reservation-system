namespace Domain.Entities;

public class Movie:BaseEntity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int DurationMinutes { get; set; }
    public string Genre { get; set; }
    public decimal Rating { get; set; }
    public DateTime ReleaseDate { get; set; }
}