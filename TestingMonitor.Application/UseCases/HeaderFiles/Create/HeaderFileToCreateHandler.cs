using MediatR;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Create;

internal sealed class HeaderFileToCreateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToCreateCommand, Guid>
{
    public async Task<Guid> Handle(HeaderFileToCreateCommand request, CancellationToken cancellationToken)
    {
        var headerFile = new HeaderFile
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        var path = await fileProvider.CreateWithContent(request.Content, headerFile.Id, cancellationToken);

        if (path == null)
        {
            ErrorCode.ErrorWithFileSaving.Throw();
        }

        headerFile.Path = path!;

        await dbContext.HeaderFiles.AddAsync(headerFile, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return headerFile.Id;
    }
}
