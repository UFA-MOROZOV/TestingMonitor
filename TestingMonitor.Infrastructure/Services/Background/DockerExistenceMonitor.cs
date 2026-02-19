using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Infrastructure.Services.Background;

/// <summary>
/// Серсвис проверки существования докера.
/// </summary>
internal class DockerExistenceMonitor(IServiceScopeFactory serviceScopeFactory, ILogger<DockerExistenceMonitor> logger) : BackgroundService
{
    private readonly TimeSpan Delay = TimeSpan.FromMinutes(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();
                var dockerManager = scope.ServiceProvider.GetRequiredService<IDockerManager>();

                var compilers = await dbContext.Compilers
                    .ToListAsync(stoppingToken);

                var existence = await dockerManager.CheckDockersAsync(compilers
                    .Select(x => x.ImageName)
                    .ToList(), stoppingToken);

                foreach (var compiler in compilers)
                {
                    compiler.HasDockerLocally = existence[compiler.ImageName];
                }

                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError("Не удалось проверить существование образов: {error}", ex);
            }

            await Task.Delay(Delay, stoppingToken);
        }
    }
}
