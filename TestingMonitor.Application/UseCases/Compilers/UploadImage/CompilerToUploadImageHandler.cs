using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.UploadImage;

public sealed class CompilerToUploadImageHandler(IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilerToUploadImageCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToUploadImageCommand request, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (compiler == null)
        {
            ErrorCode.CompilerNotFound.Throw();
        }

        if (await dockerManager.ImageExistsAsync(compiler!.ImageName, cancellationToken))
        {
            if (!compiler.HasDockerLocally)
            {
                compiler.HasDockerLocally = true;

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            ErrorCode.CompilerStart.Throw($"Image {compiler.ImageName} already exists.");
        }

        var result = await dockerManager.LoadDockerImageAsync(request.Stream, cancellationToken);

        if (!result)
        {
            ErrorCode.CompilerStart.Throw($"Image {compiler.ImageName} cannot be downloaded.");
        }

        compiler.HasDockerLocally = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
