namespace ServiceAbstraction;

public interface IServiceManager
{
    public IMovieService MovieService { get; }
    public IGenreService GenreService { get; }
}