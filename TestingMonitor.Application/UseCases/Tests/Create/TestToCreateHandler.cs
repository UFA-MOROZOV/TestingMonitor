using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Обработчик добавления тестов.
/// </summary>
internal sealed class TestToCreateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToCreateCommand, Guid>
{
    public async Task<Guid> Handle(TestToCreateCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.TestGroups.AnyAsync(x => x.Id == request.GroupId, cancellationToken))
        {
            throw new ApiException("Группы с таким идентификатором не существует.");
        }

        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            TestGroupId = request.GroupId
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, test.Id, cancellationToken)
            ?? throw new ApiException("Не удалось сохранить файл.");

        test.Path = path;

        await dbContext.Tests.AddAsync(test, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return test.Id;
    }
}
