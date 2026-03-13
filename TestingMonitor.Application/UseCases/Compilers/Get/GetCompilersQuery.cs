using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.Compilers.Get;

/// <summary>
/// Query of getting all compilers.
/// </summary>
public sealed class GetCompilersQuery : IRequest<List<CompilerDto>>
{
    /// <summary>
    /// Does it have docker locally?
    /// </summary>
    public bool? HasDockerLocally { get; set; }

    /// <summary>
    /// Keyword
    /// </summary>
    public string? Keyword { get; set; }

    private sealed class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompilersQuery, List<CompilerDto>>
    {
        public async Task<List<CompilerDto>> Handle(GetCompilersQuery request, CancellationToken cancellationToken)
        {
            var compilersQuery = dbContext.Compilers.AsQueryable();

            if (request.HasDockerLocally.HasValue)
            {
                compilersQuery = compilersQuery
                    .Where(x => x.HasDockerLocally == request.HasDockerLocally);
            }

            if (request.Keyword != null)
            {
                compilersQuery = compilersQuery.Where(x => x.Name.ToLower().StartsWith(request.Keyword.ToLower()));
            }

            var compilers = await compilersQuery
                .OrderByDescending(x => x.HasDockerLocally)
                    .ThenBy(x => x.Name)
                        .ThenBy(x => x.Id)
                .ProjectTo<CompilerDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return compilers;
        }
    }
}
