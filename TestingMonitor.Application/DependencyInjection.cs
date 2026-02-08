using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TestingMonitor.Application.Profiles;

namespace TestingMonitor.Application;

public static class DependencyInjection
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
    }
}

