using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.Tests.UpdateContent;

namespace TestingMonitor.Application.UseCases.Tests.Delete;

/// <summary>
/// Обработчик обновления теста.
/// </summary>
internal sealed class TestToUpdateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToUpdateCommand, Unit>
{
    public async Task<Unit> Handle(TestToUpdateCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (test == null)
        {
            throw new ApiException("Теста с данным идентификатором нет в базе.");
        }

        await fileProvider.UpdateContent(test.Path, request.Content, cancellationToken);

        dbContext.Tests.Remove(test);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
