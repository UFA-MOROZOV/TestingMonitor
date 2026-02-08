using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Interfaces;

public interface IDbContext
{
    DbSet<Compiler> Compilers { get; }

    DbSet<Test> Tests { get; }

    DbSet<TestGroup> TestGroups { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
