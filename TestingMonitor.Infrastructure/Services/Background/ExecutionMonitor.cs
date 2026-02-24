using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Infrastructure.Services.Background;

internal sealed class ExecutionMonitor(IServiceProvider serviceProvider, ILogger<ExecutionMonitor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IDbContext>();

                var taskExecutor = scope.ServiceProvider.GetRequiredService<TaskExecutor>();

                var task = await dbContext.CompilerTasks
                    .OrderBy(t => t.DateOfCreation)
                    .Where(t => t.DateOfCompletion == null)
                    .FirstOrDefaultAsync(stoppingToken);

                if (task != null)
                {
                    await taskExecutor.ExecuteTaskAsync(task.Id, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка выполнения: {ex}", ex.Message);
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}
