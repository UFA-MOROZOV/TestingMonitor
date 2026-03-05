using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.Delete;

internal sealed class TestToDeleteHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(TestToDeleteCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (test == null)
        {
            return Unit.Value;
        }

        fileProvider.DeleteFile(test.Path, cancellationToken);

        dbContext.Tests.Remove(test);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
