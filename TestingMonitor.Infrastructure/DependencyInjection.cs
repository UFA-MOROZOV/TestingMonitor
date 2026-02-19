using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Infrastructure.Extensions;
using TestingMonitor.Infrastructure.Persistence;
using TestingMonitor.Infrastructure.Services;
using TestingMonitor.Infrastructure.Services.Background;

namespace TestingMonitor.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IDbContext, AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("MainDb")));

        services.AddScoped<AppDbContextInitializer>();
        services.AddSingleton<IDockerManager, DockerManager>();
        services.AddHostedService<DockerExistenceMonitor>();
        services.AddTransient<IFileProvider, FileProvider>();
    }
}

