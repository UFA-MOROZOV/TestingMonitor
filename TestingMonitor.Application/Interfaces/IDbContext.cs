using Microsoft.EntityFrameworkCore;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Interfaces;

public interface IDbContext
{
    DbSet<TestEntity> TestEntities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
