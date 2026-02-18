using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Persistence;

internal sealed class AppDbContext : DbContext, IDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<Compiler> Compilers => Set<Compiler>();

    public DbSet<Test> Tests => Set<Test>();

    public DbSet<TestGroup> TestGroups => Set<TestGroup>();

    public DbSet<TestToHeaderFile> TestToHeaderFiles => Set<TestToHeaderFile>();

    public DbSet<HeaderFile> HeaderFiles => Set<HeaderFile>();
}

