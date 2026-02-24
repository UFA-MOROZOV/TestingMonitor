using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Interfaces;

public interface IDbContext
{
    DbSet<Compiler> Compilers { get; }

    DbSet<Test> Tests { get; }

    DbSet<TestGroup> TestGroups { get; }

    DbSet<TestToHeaderFile> TestToHeaderFiles { get; }

    DbSet<HeaderFile> HeaderFiles { get; }

    DbSet<CompilerTask> CompilerTasks { get; }

    DbSet<TestExecution> TestExecutions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
