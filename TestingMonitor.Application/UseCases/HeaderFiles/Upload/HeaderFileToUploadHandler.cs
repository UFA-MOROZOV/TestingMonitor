using MediatR;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Upload;

internal sealed class HeaderFileToUploadHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToUploadCommand, Guid>
{
    public async Task<Guid> Handle(HeaderFileToUploadCommand request, CancellationToken cancellationToken)
    {
        var HeaderFile = new HeaderFile
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, HeaderFile.Id, cancellationToken);

        if (path == null)
        {
            ErrorCode.ErrorWithFileSaving.Throw();
        }

        HeaderFile.Path = path!;

        await dbContext.HeaderFiles.AddAsync(HeaderFile, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return HeaderFile.Id;
    }
}
