using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Helpers;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

/// <summary>
/// Обработчик загрузки зип с тестами. 
/// </summary>
internal sealed class TestGroupToUploadHandler(IDbContext dbContext, IFileProvider fileProvider)
    : IRequestHandler<TestGroupToUploadCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToUploadCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId.HasValue && !await dbContext.TestGroups.AnyAsync(x => request.GroupId == x.Id,
            cancellationToken))
        {
            throw new ApiException("Нет группы родителя.");
        }

        var processor = new ZipTestArchiveProcessor(dbContext, fileProvider);
        await processor.ProcessAsync(request.Stream, request.GroupId, cancellationToken);

        return Unit.Value;
    }
}