using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteTests;

internal sealed class CompilerToExecuteTestHandler(IDbContext dbContext) : IRequestHandler<CompilerToExecuteTestCommand, Guid>
{
    public async Task<Guid> Handle(CompilerToExecuteTestCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Compilers.AnyAsync(x => x.Id == request.CompilerId, cancellationToken))
        {
            ErrorCode.CompilerNotFound.Throw();
        }

        if (!(request.TestGroupId == null ^ request.TestId == null))
        {
            ErrorCode.CompilerStart.Throw("You must choose only either test group id or test id");
        }

        if (request.TestGroupId != null && !await dbContext.TestGroups.AnyAsync(x => x.Id == request.TestGroupId, cancellationToken))
        {
            ErrorCode.TestGroupNotFound.Throw();
        }

        if (request.TestId != null && !await dbContext.Tests.AnyAsync(x => x.Id == request.TestId, cancellationToken))
        {
            ErrorCode.TestNotFound.Throw();
        }

        var executionTask = new CompilerTask
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
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
