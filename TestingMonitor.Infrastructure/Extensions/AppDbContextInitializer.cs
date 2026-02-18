namespace TestingMonitor.Infrastructure.Extensions;

using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Infrastructure.Persistence;

internal sealed class AppDbContextInitializer(ILogger<AppDbContextInitializer> logger, AppDbContext dbContext)
{
    internal async Task InitializeAsync()
    {
        try
        {
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
                await dbContext.Database.MigrateAsync();

            Seed();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    internal void Seed()
    {
        var compilersJson = File.ReadAllText("compilers.json");

        var compilers = JsonSerializer.Deserialize<List<Compiler>>(compilersJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });

        if (compilers == null)
        {
            return;
        }

        var changed = false;

        foreach (var compiler in compilers)
        {
            if (!dbContext.Compilers.Any(x => x.Name == compiler.Name && x.Version == compiler.Version))
            {
                changed = true;
                dbContext.Compilers.Add(compiler);
            }
        }

        if (changed)
        {
            dbContext.SaveChanges();
        }
    }
}
