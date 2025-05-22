using Microsoft.EntityFrameworkCore;
using OCP5.Data;
using OCP5.Data.Seeders;

namespace OCP5.Extensions;

public static class DatabaseInitializerExtension
{
    public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            using var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
            applicationDbContext.Database.Migrate();
            DataSeeder.Initialize(services, config);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Une s'est produite lors de l'initialisation de la base de données et/ou l'ajout des données par défaut.");
        }

        return app;
    }
}