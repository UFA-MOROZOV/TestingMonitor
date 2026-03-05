using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Create;

internal sealed class TestGroupToCreateHandler(IDbContext dbContext) : IRequestHandler<TestGroupToCreateCommand, Guid>
{
    public async Task<Guid> Handle(TestGroupToCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.ParentGroupId.HasValue
            && !await dbContext.TestGroups.AnyAsync(x => x.Id == request.ParentGroupId, cancellationToken))
        {
            ErrorCode.TestGroupNotFound.Throw();
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
