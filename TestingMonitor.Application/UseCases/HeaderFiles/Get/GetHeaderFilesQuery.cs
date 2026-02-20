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

    /// <summary>
    /// Идентификатор группы тестов.
    /// </summary>
    public Guid? TestGroupId { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid? TestId { get; set; }

    private class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetHeaderFilesQuery, GetHeaderFilesResponse>
    {
        public async Task<GetHeaderFilesResponse> Handle(GetHeaderFilesQuery request, CancellationToken cancellationToken)
        {
            var headersFileQyery = dbContext.HeaderFiles.AsQueryable();

            if (request.Search != null)
            {
                headersFileQyery = headersFileQyery
                    .Where(x => x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }
            
            if (request.TestGroupId != null)
            {
                headersFileQyery = headersFileQyery
                    .Where(x => x.TestGroups.Any(y => y.TestGroupId == request.TestGroupId));
            }

            if (request.TestId != null)
            {
                headersFileQyery = headersFileQyery
                    .Where(x => x.Tests.Any(y => y.TestId == request.TestId));
            }

            var headerFiles = await headersFileQyery
                .ProjectTo<HeaderFileDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetHeaderFilesResponse
            {
                HeaderFiles = headerFiles,
            };
        }
    }
}
