using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Tests.Get;

/// <summary>
/// Запрос на получение тестов.
/// </summary>
public sealed class GetTestsQuery : IRequest<GetTestsResponse>
{
    /// <summary>
    /// Папка, в которой ищем.
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// Глобальный ли поиск.
    /// </summary>
    public bool Global { get; set; }

    /// <summary>
    /// Поиск по ключевому слову.
    /// </summary>
    public string? Search { get; set; }

    private class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetTestsQuery, GetTestsResponse>
    {
        public async Task<GetTestsResponse> Handle(GetTestsQuery request, CancellationToken cancellationToken)
        {
            var groups = await dbContext.TestGroups
                .Where(x => (request.Global || request.ParentId == x.ParentGroupId))
                .ProjectTo<ItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var tests = await dbContext.Tests
                .Where(x => (request.Global || request.ParentId == x.TestGroupId))
                .ProjectTo<ItemDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (request.Search != null)
            {
                groups = groups
                    .Where(x => x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                tests = tests
                    .Where(x => x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return new GetTestsResponse
            {
                SubGroups = groups,
                Tests = tests
            };
        }
    }
}
