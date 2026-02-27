using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Compilers.Update;

/// <summary>
/// Обработчик загрузки докера компилятора.
/// </summary>
internal sealed class CompilerToUpdateHandler(IDbContext dbContext, IMapper mapper)
    : IRequestHandler<CompilerToUpdateCommand, Unit>
{
    /// <inheritdoc/>
    public async Task<Unit> Handle(CompilerToUpdateCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("No compiler found");
        }

        mapper.Map(compiler, compiler);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
