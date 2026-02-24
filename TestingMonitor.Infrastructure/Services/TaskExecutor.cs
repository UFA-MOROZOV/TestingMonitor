using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Infrastructure.Services;

/// <summary>
/// Сервис выполнения задач.
/// </summary>
internal sealed class TaskExecutor(IDbContext dbContext, IDockerManager dockerManager)
{
    /// <summary>
    /// Выполняет задачу.
    /// </summary>
    public async Task ExecuteTaskAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await dbContext.ExecutionTasks
            .FirstOrDefaultAsync(x => x.Id == taskId, cancellationToken);

        if (task == null)
        {
            return;
        }

        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == task.CompilerId, cancellationToken);

        if (compiler == null)
        {
            return;
        }

        task.DateOfStart = DateTime.UtcNow;

        if (task.TestId.HasValue)
        {
            await ExecuteTestAsync(compiler, task.Id, task.TestId.Value, [], cancellationToken);
        }
        else if (task.TestGroupId.HasValue)
        {
            await ExecuteTestGroupAsync(compiler, task.Id, task.TestGroupId.Value, [], cancellationToken);
        }

        task.DateOfEnd = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ExecuteTestAsync(Compiler compiler, Guid taskId, Guid testId, List<Guid> headerIds, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .Include(x => x.HeaderFiles)
            .FirstOrDefaultAsync(x => x.Id == testId, cancellationToken);

        if (test == null)
        {
            return;
        }

        var testHeaderIds = test.HeaderFiles
            .Select(x => x.HeaderId)
            .Concat(headerIds)
            .Distinct();

        var headers = await dbContext.HeaderFiles
            .Where(x => testHeaderIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var testRunId = Guid.NewGuid();
        
        var output = await dockerManager.ExecuteCodeAsync(compiler, testRunId, test, headers, cancellationToken);

        var testExecution = new TestExecution
        {
            Id = testRunId,
            ExecutionTaskId = taskId,
            TestId = testId,
            ErrorMessage = output.Message,
            DurationInSeconds = output.Duration.Seconds,
            IsSuccessful = string.IsNullOrEmpty(output.Message),
        };

        await dbContext.TestExecutions.AddAsync(testExecution, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task ExecuteTestGroupAsync(Compiler compiler, Guid taskId, Guid testGroupId, List<Guid> headerIds, CancellationToken cancellationToken)
    {
        var testGroup = await dbContext.TestGroups
            .Include(x => x.HeaderFiles)
            .Include(x => x.Tests)
            .Include(x => x.SubGroups)
            .FirstOrDefaultAsync(x => x.Id == testGroupId, cancellationToken);

        if (testGroup == null)
        {
            return;
        }

        var testHeaderIds = testGroup.HeaderFiles
            .Select(x => x.HeaderId)
            .Concat(headerIds)
            .Distinct()
            .ToList();

        var headers = await dbContext.HeaderFiles
            .Where(x => testHeaderIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        foreach (var test in testGroup.Tests)
        {
            await ExecuteTestAsync(compiler, taskId, test.Id, testHeaderIds, cancellationToken);
        }

        foreach (var subGroup in testGroup.SubGroups)
        {
            await ExecuteTestGroupAsync(compiler, taskId, subGroup.Id, testHeaderIds, cancellationToken);
        }
    }
}
