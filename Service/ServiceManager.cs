using ServiceAbstraction;

namespace Service;

public class ServiceManager(
    Func<IMovieService> movieFactory,
    Func<IGenreService> genreFactory,
    Func<ISeatService> seatFactory,
    Func<IScheduleService> scheduleFactory,
    Func<ICacheService> cacheFactory,
    Func<IEmailService> emailFactory,
    Func<IAuthenticationService> authenticationFactory) : IServiceManager
{
    public IMovieService MovieService => movieFactory.Invoke();
    public IGenreService GenreService => genreFactory.Invoke();
    public ISeatService SeatService => seatFactory.Invoke();
    public IScheduleService ScheduleService => scheduleFactory.Invoke();
    public ICacheService CacheService => cacheFactory.Invoke();
    public IEmailService EmailService => emailFactory.Invoke();
    public IAuthenticationService AuthenticationService => authenticationFactory.Invoke();
}