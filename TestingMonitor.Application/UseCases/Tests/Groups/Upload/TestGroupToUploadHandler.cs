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
        if (request.ParentGroupId.HasValue && !await dbContext.TestGroups.AnyAsync(x => request.ParentGroupId == x.Id,
            cancellationToken))
        {
            throw new ApiException("Нет группы родителя.");
        }

        var processor = new ZipTestArchiveProcessor(dbContext, fileProvider);
        await processor.ProcessAsync(request.Stream, request.ParentGroupId, cancellationToken);

        return Unit.Value;
    }
}