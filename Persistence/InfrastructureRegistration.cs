using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Data.DataSeeding;
using Persistence.Repos;
using StackExchange.Redis;

namespace Persistence;

public static class InfrastructureRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MovieDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
        services.AddScoped<ICacheRepo, CacheRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IScheduleRepo, ScheduleRepo>();
        services.AddScoped<IDataSeeding, DataSeeding>();
        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")!));
        services.AddDbContext<MovieIdentityDbContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
        });
        services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<MovieIdentityDbContext>();
        return services;
    }
}