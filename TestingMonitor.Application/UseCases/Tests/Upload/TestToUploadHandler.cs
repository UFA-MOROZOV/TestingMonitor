using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.Tests.Upload;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Обработчик добавления тестов.
/// </summary>
internal sealed class TestToUploadHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<TestToUploadCommand, Guid>
{
    public async Task<Guid> Handle(TestToUploadCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.TestGroups.AnyAsync(x => x.Id == request.GroupId, cancellationToken))
        {
            ErrorCode.TestGroupNotFound.Throw();
        }

        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            TestGroupId = request.GroupId
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, test.Id, cancellationToken);

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
