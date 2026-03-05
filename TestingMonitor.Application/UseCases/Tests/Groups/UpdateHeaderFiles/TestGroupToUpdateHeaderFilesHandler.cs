using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.Groups.UpdateHeaderFiles;

internal sealed class TestGroupToUpdateHeaderFilesHandler(IDbContext dbContext) : IRequestHandler<TestGroupToUpdateHeaderFilesCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToUpdateHeaderFilesCommand request, CancellationToken cancellationToken)
    {
        var testGroup = await dbContext.TestGroups
            .Include(x => x.HeaderFiles)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (testGroup == null)
        {
            ErrorCode.TestNotFound.Throw();
        }

        request.HeaderIds = request.HeaderIds.Distinct().ToList();

        var existingHeaders = await dbContext.HeaderFiles
            .Where(x => request.HeaderIds.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        if (existingHeaders.Count != request.HeaderIds.Count)
        {
            ErrorCode.HeaderFileNotFound.Throw();
        }

        testGroup!.HeaderFiles = testGroup.HeaderFiles
            .Where(x => existingHeaders.Contains(x.HeaderId))
            .ToList();

        var headersToAdd = request.HeaderIds.Except(testGroup.HeaderFiles.Select(x => x.HeaderId));

        foreach (var header in headersToAdd)
        {
            testGroup.HeaderFiles.Add(new TestGroupToHeaderFile
            {
                HeaderId = header,
                TestGroupId = testGroup.Id
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
