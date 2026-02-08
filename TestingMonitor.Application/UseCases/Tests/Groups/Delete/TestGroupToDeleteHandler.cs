using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Delete;

/// <summary>
/// Обработчик удаления группы тестов.
/// </summary>
internal sealed class TestGroupToDeleteHandler(IDbContext dbContext, IFileProvider fileProvider)
    : IRequestHandler<TestGroupToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToDeleteCommand request, CancellationToken cancellationToken)
    {
        await DeleteFolderAsync(request.Id, cancellationToken);

        return Unit.Value;
    }

    private async Task DeleteFolderAsync(Guid id, CancellationToken cancellationToken)
    {
        var testGroup = await dbContext.TestGroups
            .Include(x => x.SubGroups)
            .FirstOrDefaultAsync(x => id == x.Id, cancellationToken);

        if (testGroup == null)
        {
            return;
        }

        foreach (var test in testGroup.Tests)
        {
            await fileProvider.DeleteFileAsync(test.Path, cancellationToken);
        }

        foreach (var subgroup in testGroup.SubGroups)
        {
            await DeleteFolderAsync(subgroup.Id, cancellationToken);
        }

        return;
    }
}
