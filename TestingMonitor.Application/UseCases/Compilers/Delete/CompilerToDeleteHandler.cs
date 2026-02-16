using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

public sealed class CompilerToDeleteHandler (IDockerManager dockerManager, IDbContext dbContext) 
    : IRequestHandler<CompilerToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToDeleteCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("No compiler found");
        }

        if (command.DeleteDocker && compiler.HasDockerLocally)
        {
            if (!await dockerManager.DeleteDockerImageAsync(compiler, cancellationToken))
            {
                throw new ApiException($"Не получилось удалить образ.");
            }
        }

        dbContext.Compilers.Remove(compiler);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
