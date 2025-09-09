namespace Domain.Entities;

public class MovieGenre
{
    public Movie Movie { get; set; }
    public int MovieId { get; set; }
    public Genre Genre { get; set; }
    public int GenreId { get; set; }
}