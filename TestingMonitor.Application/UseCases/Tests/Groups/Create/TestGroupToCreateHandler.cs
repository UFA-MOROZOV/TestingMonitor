using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Create;

/// <summary>
/// Обработчик создания группы тестов.
/// </summary>
internal sealed class TestGroupToCreateHandler(IDbContext dbContext) : IRequestHandler<TestGroupToCreateCommand, Guid>
{
    public async Task<Guid> Handle(TestGroupToCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentGroupId.HasValue
            && !await dbContext.TestGroups.AnyAsync(x => x.Id == request.ParentGroupId, cancellationToken))
        {
            throw new ApiException("Нет группы родителя.");
        }

        var newGroup = new TestGroup
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            ParentGroupId = request.ParentGroupId,
        };

        await dbContext.TestGroups.AddAsync(newGroup, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return newGroup.Id;
    }
}
