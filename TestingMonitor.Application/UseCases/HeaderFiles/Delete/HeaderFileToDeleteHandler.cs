using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Delete;

/// <summary>
/// Обработчик удаления header файла.
/// </summary>
internal sealed class HeaderFileToDeleteHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(HeaderFileToDeleteCommand request, CancellationToken cancellationToken)
    {
        var headerFile = await dbContext.HeaderFiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (headerFile == null)
        {
            return Unit.Value;
        }

        await fileProvider.DeleteFileAsync(headerFile.Path, cancellationToken);

        dbContext.HeaderFiles.Remove(headerFile);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
