using MediatR;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Upload;

/// <summary>
/// Обработчик добавления header файла.
/// </summary>
internal sealed class HeaderFileToUploadHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToUploadCommand, Guid>
{
    public async Task<Guid> Handle(HeaderFileToUploadCommand request, CancellationToken cancellationToken)
    {
        var HeaderFile = new HeaderFile
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, HeaderFile.Id, cancellationToken)
            ?? throw new ApiException("Не удалось сохранить файл.");

        HeaderFile.Path = path;

        await dbContext.HeaderFiles.AddAsync(HeaderFile, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return HeaderFile.Id;
    }
}
