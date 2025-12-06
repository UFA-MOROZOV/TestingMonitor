using Microsoft.EntityFrameworkCore;

namespace TestingMonitor.Infrastructure.Persistence;

internal class DatabaseInitializer
{
    private readonly AppDbContext _context;

    internal DatabaseInitializer(AppDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();
    }
}