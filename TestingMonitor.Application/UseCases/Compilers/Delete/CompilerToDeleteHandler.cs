using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

internal sealed class CompilerToDeleteHandler(IDockerManager dockerManager, IDbContext dbContext)
    : IRequestHandler<CompilerToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToDeleteCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            ErrorCode.CompilerAlreadyExists.Throw();
        }

        if (compiler!.HasDockerLocally && await dockerManager.ImageExistsAsync(compiler.ImageName, cancellationToken))
        {
            if (!await dockerManager.DeleteDockerImageAsync(compiler, cancellationToken))
            {
                ErrorCode.CannotDeleteImage.Throw();
            }
        }

        dbContext.Compilers.Remove(compiler);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
