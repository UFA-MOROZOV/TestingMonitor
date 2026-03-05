using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.Update;

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
            ErrorCode.CompilerNotFound.Throw();
        }

        mapper.Map(compiler, compiler);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
