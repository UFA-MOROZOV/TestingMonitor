using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Tests.UpdateHeaderFiles;

internal sealed class TestToUpdateHeaderFilesHandler(IDbContext dbContext) : IRequestHandler<TestToUpdateHeaderFilesCommand, Unit>
{
    public async Task<Unit> Handle(TestToUpdateHeaderFilesCommand request, CancellationToken cancellationToken)
    {
        var test = await dbContext.Tests
            .Include(x => x.HeaderFiles)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (test == null)
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

        test!.HeaderFiles = test.HeaderFiles
            .Where(x => existingHeaders.Contains(x.HeaderId))
            .ToList();

        var headersToAdd = request.HeaderIds.Except(test.HeaderFiles.Select(x => x.HeaderId));

        foreach (var header in headersToAdd)
        {
            test.HeaderFiles.Add(new TestToHeaderFile
            {
                HeaderId = header,
                TestId = test.Id
            });
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
