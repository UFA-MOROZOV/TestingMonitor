using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.HeaderFiles.GetContent;

/// <summary>
/// Query of getting header file content.
/// </summary>
public sealed class GetHeaderFileContentByIdQuery(Guid id) : IRequest<GetHeaderFileContentByIdResponse>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IFileProvider fileProvider) : IRequestHandler<GetHeaderFileContentByIdQuery, GetHeaderFileContentByIdResponse>
    {
        public async Task<GetHeaderFileContentByIdResponse> Handle(GetHeaderFileContentByIdQuery request, CancellationToken cancellationToken)
        {
            var headerFile = await dbContext.HeaderFiles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (headerFile == null)
            {
                ErrorCode.HeaderFileNotFound.Throw();
            }

            return new GetHeaderFileContentByIdResponse
            {
                Id = headerFile!.Id,
                Name = headerFile.Name,
                Content = await fileProvider.GetContent(headerFile.Path, cancellationToken),
            };
        }
    }
}
