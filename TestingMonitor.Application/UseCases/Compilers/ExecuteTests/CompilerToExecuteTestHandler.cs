using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteTests;

/// <summary>
/// Обработчик команды тестирования компилятора.
/// </summary>
internal sealed class CompilerToExecuteTestHandler (IDbContext dbContext) : IRequestHandler<CompilerToExecuteTestCommand, Guid>
{
    public async Task<Guid> Handle(CompilerToExecuteTestCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Compilers.AnyAsync(x => x.Id == request.CompilerId, cancellationToken))
        {
            throw new ApiException("Компилятор с таким идентификатором не существует.");
        }

        if (!(request.TestGroupId == null ^ request.TestId == null))
        {
            throw new ApiException("Некорректная цель сканирования.");
        }

        if (request.TestGroupId != null && !await dbContext.TestGroups.AnyAsync(x => x.Id == request.TestGroupId, cancellationToken))
        {
            throw new ApiException("Группа с таким идентификатором не существует.");
        }

        if (request.TestId != null && !await dbContext.Tests.AnyAsync(x => x.Id == request.TestId, cancellationToken))
        {
            throw new ApiException("Тест с таким идентификатором не существует.");
        }

        var executionTask = new CompilerTask
        {
            Id = Guid.NewGuid(),
            CompilerId = request.CompilerId,
            TestGroupId = request.TestGroupId,
            TestId = request.TestId,
            DateOfCreation = DateTime.UtcNow,
        };

        await dbContext.CompilerTasks.AddAsync(executionTask, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return executionTask.Id;
    }
}
