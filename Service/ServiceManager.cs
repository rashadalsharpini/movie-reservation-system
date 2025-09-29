using ServiceAbstraction;

namespace Service;

public class ServiceManager(Func<IMovieService> movieFactory,
    Func<IGenreService> genreFactory, Func<ISeatService> seatFactory):IServiceManager
{
    public IMovieService MovieService => movieFactory.Invoke();
    public IGenreService GenreService => genreFactory.Invoke();
    public ISeatService SeatService => seatFactory.Invoke();
}