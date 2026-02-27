using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.HeaderFiles.UpdateContent;

/// <summary>
/// Обработчик обновления теста.
/// </summary>
internal sealed class HeaderFileToUpdateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToUpdateCommand, Unit>
{
    public async Task<Unit> Handle(HeaderFileToUpdateCommand request, CancellationToken cancellationToken)
    {
        var headerFile = await dbContext.HeaderFiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (headerFile == null)
        {
            throw new ApiException("Header файла с данным идентификатором нет в базе.");
        }

        await fileProvider.UpdateContent(headerFile.Path, request.Content, cancellationToken);

        dbContext.HeaderFiles.Remove(headerFile);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
