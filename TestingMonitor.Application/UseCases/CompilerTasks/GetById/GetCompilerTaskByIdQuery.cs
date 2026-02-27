using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.CompilerTasks.GetById;

/// <summary>
/// Запрос получения задачи компилятора.
/// </summary>
public sealed class GetCompilerTaskByIdQuery(Guid id) : IRequest<GetCompilerTaskByIdResponse>
{
    public Guid Id { get; set; } = id;

    private class Handler(IDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompilerTaskByIdQuery, GetCompilerTaskByIdResponse>
    {
        public async Task<GetCompilerTaskByIdResponse> Handle(GetCompilerTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var compilerTask = await dbContext.CompilerTasks
                .ProjectTo<GetCompilerTaskByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (compilerTask == null)
            {
                throw new ApiException("Задача компилятора с такими данными не существует.");
            }

            return compilerTask;
        }
    }
}
