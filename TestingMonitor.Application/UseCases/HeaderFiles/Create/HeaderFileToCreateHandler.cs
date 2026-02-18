using MediatR;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Create;

/// <summary>
/// Обработчик добавления header файлов.
/// </summary>
internal sealed class HeaderFileToCreateHandler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<HeaderFileToCreateCommand, Guid>
{
    public async Task<Guid> Handle(HeaderFileToCreateCommand request, CancellationToken cancellationToken)
    {
        var headerFile = new HeaderFile
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        var path = await fileProvider.UploadFileAsync(request.Stream, headerFile.Id, cancellationToken)
            ?? throw new ApiException("Не удалось сохранить файл.");

        headerFile.Path = path;

        await dbContext.HeaderFiles.AddAsync(headerFile, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return headerFile.Id;
    }
}
