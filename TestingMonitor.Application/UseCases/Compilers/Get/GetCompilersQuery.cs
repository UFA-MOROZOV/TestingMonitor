using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.Compilers.Get;

/// <summary>
/// Запрос на получение компиляторов.
/// </summary>
public sealed class GetCompilersQuery : IRequest<List<CompilerDto>>
{
    private sealed class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompilersQuery, List<CompilerDto>>
    {
        public async Task<List<CompilerDto>> Handle(GetCompilersQuery request, CancellationToken cancellationToken)
        {
            var compilers = await dbContext.Compilers
                .ProjectTo<CompilerDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return compilers;
        }
    }
}
