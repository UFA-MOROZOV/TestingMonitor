using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.HeaderFiles.UpdateContent;

internal sealed class HeaderFileToUpdateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToUpdateCommand, Unit>
{
    public async Task<Unit> Handle(HeaderFileToUpdateCommand request, CancellationToken cancellationToken)
    {
        var headerFile = await dbContext.HeaderFiles
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (headerFile == null)
        {
            ErrorCode.HeaderFileNotFound.Throw();
        }

        await fileProvider.UpdateContent(headerFile!.Path, request.Content, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
