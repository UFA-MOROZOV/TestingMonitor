using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestingMonitor.Infrastructure.Extensions;

public static class IHostExtensions
{
    public static void InitDB(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

        initializer.InitializeAsync().Wait();
    }
}