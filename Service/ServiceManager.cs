using ServiceAbstraction;

namespace Service;

public class ServiceManager(Func<IMovieService> movieFactory,
    Func<IGenreService> genreFactory):IServiceManager
{
    public IMovieService MovieService => movieFactory.Invoke();
    public IGenreService GenreService => genreFactory.Invoke();
}