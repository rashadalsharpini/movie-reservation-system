namespace ServiceAbstraction;

public interface IServiceManager
{
    public IMovieService MovieService { get; }
    public IGenreService GenreService { get; }
    public ISeatService SeatService { get; }
    public IScheduleService ScheduleService { get; }
    public ICacheService CacheService { get; }
}