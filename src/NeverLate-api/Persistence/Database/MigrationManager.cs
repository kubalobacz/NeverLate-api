using Microsoft.EntityFrameworkCore;

namespace NeverLate_api.Persistence.Database;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var appContext = scope.ServiceProvider.GetRequiredService<NeverLateContext>();
        appContext.Database.Migrate();

        return webApp;
    }
}