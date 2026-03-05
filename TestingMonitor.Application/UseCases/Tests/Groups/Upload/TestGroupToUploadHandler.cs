using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Helpers;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

internal sealed class TestGroupToUploadHandler(IDbContext dbContext, IFileProvider fileProvider)
    : IRequestHandler<TestGroupToUploadCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToUploadCommand request, CancellationToken cancellationToken)
    {
        if (request.GroupId.HasValue && !await dbContext.TestGroups.AnyAsync(x => request.GroupId == x.Id,
            cancellationToken))
        {
            ErrorCode.TestGroupNotFound.Throw();
        }

        var processor = new ZipTestArchiveProcessor(dbContext, fileProvider);
        await processor.ProcessAsync(request.Stream, request.GroupId, cancellationToken);

        return Unit.Value;
    }
}