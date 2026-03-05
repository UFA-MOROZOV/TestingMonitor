using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Create;

internal sealed class TestToCreateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToCreateCommand, Guid>
{
    public async Task<Guid> Handle(TestToCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId.HasValue
            && !await dbContext.TestGroups.AnyAsync(x => x.Id == request.GroupId, cancellationToken))
        {
            ErrorCode.TestGroupAlreadyExists.Throw();
        }

        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            TestGroupId = request.GroupId
        };

        var path = await fileProvider.CreateWithContent(request.Content, test.Id, cancellationToken);

        if (path == null)
        {
            ErrorCode.ErrorWithFileSaving.Throw();
        }

        test.Path = path!;

        await dbContext.Tests.AddAsync(test, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return test.Id;
    }
}
