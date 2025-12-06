namespace TestingMonitor.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestingMonitor.Infrastructure.Persistence;

internal class AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext dbContext)
{
    internal async Task InitializeAsync()
    {
        try
        {
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}
