namespace Domain.Entities;

public class MovieGenre
{
    public Movie Movie { get; set; }
    public Guid MovieId { get; set; }
    public Genre Genre { get; set; }
    public Guid GenreId { get; set; }
}