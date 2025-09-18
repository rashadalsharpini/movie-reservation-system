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
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}