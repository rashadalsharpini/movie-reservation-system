using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceAbstraction;

namespace Service;

public static class AppServiceRegistration
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // https://docs.automapper.io/en/stable/Configuration.html
        var config = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(AssemblyRef)); }, new LoggerFactory());
        services.AddSingleton(config.CreateMapper());
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<Func<IMovieService>>(sp => sp.GetRequiredService<IMovieService>);
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<Func<IGenreService>>(sp => sp.GetRequiredService<IGenreService>);
        services.AddScoped<Func<ISeatService>>(sp => sp.GetRequiredService<ISeatService>);
        services.AddScoped<Func<IScheduleService>>(sp => sp.GetRequiredService<IScheduleService>);
        services.AddScoped<ISeatService, SeatService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<Func<ICacheService>>(sp => sp.GetRequiredService<ICacheService>);
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}