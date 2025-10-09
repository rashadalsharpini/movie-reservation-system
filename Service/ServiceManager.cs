using ServiceAbstraction;

namespace Service;

public class ServiceManager(
    Func<IMovieService> movieFactory,
    Func<IGenreService> genreFactory,
    Func<ISeatService> seatFactory,
    Func<IScheduleService> scheduleFactory,
    Func<ICacheService> cacheFactory) : IServiceManager
{
    public IMovieService MovieService => movieFactory.Invoke();
    public IGenreService GenreService => genreFactory.Invoke();
    public ISeatService SeatService => seatFactory.Invoke();
    public IScheduleService ScheduleService => scheduleFactory.Invoke();
    public ICacheService CacheService => cacheFactory.Invoke();
}