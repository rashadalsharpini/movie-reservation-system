using Domain.Contracts;

namespace movieReservation.web.api.Extentions;

public static class WebApplicationExtentions
{
    public static async Task<WebApplication> SeedDbInitializeAsync(this WebApplication app)
    {
        using var scop=app.Services.CreateScope();
        var dbInitializer = scop.ServiceProvider.GetRequiredService<IDataSeeding>();
        await dbInitializer.DataSeedAsync();
        // await dbInitializer.IdentitySeedAsync();
        return app;
    }
}