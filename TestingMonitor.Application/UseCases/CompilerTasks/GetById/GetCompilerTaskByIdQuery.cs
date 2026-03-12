using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.CompilerTasks.GetById;

/// <summary>
/// Query of getting compiler task.
/// </summary>
public sealed class GetCompilerTaskByIdQuery(Guid id) : IRequest<CompilerTaskByIdDto>
{
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompilerTaskByIdQuery, CompilerTaskByIdDto>
    {
        public async Task<CompilerTaskByIdDto> Handle(GetCompilerTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var compilerTask = await dbContext.CompilerTasks
                .ProjectTo<CompilerTaskByIdDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (compilerTask == null)
            {
                ErrorCode.CompilerTaskNotFound.Throw();
            }

            return compilerTask!;
        }
    }
}
