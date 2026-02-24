using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.TaskExecutions.Get;

/// <summary>
/// Запрос на получение компиляторов.
/// </summary>
public sealed class GetCompilerTasksQuery : IRequest<List<CompilerTaskDto>>
{
    private sealed class Handler (IDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompilerTasksQuery, List<CompilerTaskDto>>
    {
        public async Task<List<CompilerTaskDto>> Handle(GetCompilerTasksQuery request, CancellationToken cancellationToken)
        {
            var taskExecutions = await dbContext.CompilerTasks
                .ProjectTo<CompilerTaskDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return taskExecutions;
        }
    }
}
