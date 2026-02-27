using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.HeaderFiles.GetContent;

/// <summary>
/// Запрос на получение содержимого header файла.
/// </summary>
public sealed class GetHeaderFileContentByIdQuery(Guid id) : IRequest<GetHeaderFileContentByIdResponse>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IFileProvider fileProvider)
    {
        public async Task<GetHeaderFileContentByIdResponse> Handle(GetHeaderFileContentByIdQuery request, CancellationToken cancellationToken)
        {
            var HeaderFile = await dbContext.HeaderFiles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (HeaderFile == null)
            {
                throw new ApiException("Теста с данным идентификатором нет в базе.");
            }

            return new GetHeaderFileContentByIdResponse
            {
                Id = HeaderFile.Id,
                Name = HeaderFile.Name,
                Content = await fileProvider.GetContent(HeaderFile.Path, cancellationToken),
            };
        }
    }
}
