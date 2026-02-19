using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Get;

/// <summary>
/// Запрос на получение header файлов.
/// </summary>
public sealed class GetHeaderFilesQuery : IRequest<GetHeaderFilesResponse>
{
    /// <summary>
    /// Поиск по ключевому слову.
    /// </summary>
    public string? Search { get; set; }

    private class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetHeaderFilesQuery, GetHeaderFilesResponse>
    {
        public async Task<GetHeaderFilesResponse> Handle(GetHeaderFilesQuery request, CancellationToken cancellationToken)
        {
            var headersFiles = await dbContext.HeaderFiles
                .ProjectTo<HeaderFileDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (request.Search != null)
            {
                headersFiles = headersFiles
                    .Where(x => x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return new GetHeaderFilesResponse
            {
                HeaderFiles = headersFiles,
            };
        }
    }
}
