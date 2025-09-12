namespace Domain.Entities;

public class MovieGenre
{
    public Movie Movie { get; set; } = null!;
    public Guid MovieId { get; set; }
    public Genre Genre { get; set; } = null!;
    public Guid GenreId { get; set; }
}