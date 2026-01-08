using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.Get;

/// <summary>
/// Запрос на получение компиляторов.
/// </summary>
public sealed class GetCompilersQuery : IRequest<List<CompilerDto>>
{
    private sealed class Handler (IDbContext dbContext) : IRequestHandler<GetCompilersQuery, List<CompilerDto>>
    {
        public async Task<List<CompilerDto>> Handle(GetCompilersQuery request, CancellationToken cancellationToken)
        {
            var compilers = await dbContext.Compilers.ToListAsync(cancellationToken);

            return compilers.Select(x => new CompilerDto
            {
                Id  = x.Id,
                Name = x.Name,
                Version = x.Version,
                HasDockerLocally = x.HasDockerLocally,
                CommandName = x.CommandName,
            }).ToList();
        }
    }
}
