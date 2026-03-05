using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.DeleteImage;

internal sealed class CompilerToDeleteImageHandler(IDockerManager dockerManager, IDbContext dbContext)
    : IRequestHandler<CompilerToDeleteImageCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToDeleteImageCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            ErrorCode.CompilerAlreadyExists.Throw();
        }

        if (!compiler!.HasDockerLocally)
        {
            return Unit.Value;
        }

        if (compiler.HasDockerLocally && await dockerManager.ImageExistsAsync(compiler.ImageName, cancellationToken))
        {
            if (!await dockerManager.DeleteDockerImageAsync(compiler, cancellationToken))
            {
                ErrorCode.CannotDeleteImage.Throw();
            }
        }

        compiler.HasDockerLocally = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
