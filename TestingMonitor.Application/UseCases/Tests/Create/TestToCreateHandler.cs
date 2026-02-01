using MediatR;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Обработчик добавления тестов.
/// </summary>
internal sealed class TestToCreateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToCreateCommand, Unit>
{
    public async Task<Unit> Handle(TestToCreateCommand request, CancellationToken cancellationToken)
    {
        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, test.Id);

        test.Path = path;

        await dbContext.Tests.AddAsync(test, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
