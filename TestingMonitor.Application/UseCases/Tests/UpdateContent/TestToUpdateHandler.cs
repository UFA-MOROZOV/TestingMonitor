using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.Tests.UpdateContent;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Delete;

internal sealed class TestToUpdateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToUpdateCommand, Unit>
{
    public async Task<Unit> Handle(TestToUpdateCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (test == null)
        {
            ErrorCode.TestNotFound.Throw();
        }

        await fileProvider.UpdateContent(test!.Path, request.Content, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
