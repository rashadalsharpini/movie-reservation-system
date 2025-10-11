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
        services.AddScoped<ISeatService, SeatService>();
        services.AddScoped<Func<ISeatService>>(sp => sp.GetRequiredService<ISeatService>);
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<Func<IScheduleService>>(sp => sp.GetRequiredService<IScheduleService>);
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<Func<ICacheService>>(sp => sp.GetRequiredService<ICacheService>);
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<Func<IEmailService>>(sp => sp.GetRequiredService<IEmailService>);
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<Func<IAuthenticationService>>(sp => sp.GetRequiredService<IAuthenticationService>);
        services.AddScoped<IServiceManager, ServiceManager>();
        return services;
    }
}