using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.Delete;

/// <summary>
/// Обработчик удаления теста.
/// </summary>
internal class TestToDeleteHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(TestToDeleteCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (test == null)
        {
            return Unit.Value;
        }

        await fileProvider.DeleteFileAsync(test.Path, cancellationToken);

        dbContext.Tests.Remove(test);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
