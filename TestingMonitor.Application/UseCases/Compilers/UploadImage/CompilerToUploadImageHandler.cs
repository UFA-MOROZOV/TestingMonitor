using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.UploadImage;

/// <summary>
/// Обработчик загрузки образа компилятора.
/// </summary>
public sealed class CompilerToUploadImageHandler (IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilertToUploadImageCommand, Unit>
{
    public async Task<Unit> Handle(CompilertToUploadImageCommand request, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("Компилятор с такими данными не существует.");
        }

        if (await dockerManager.ImageExistsAsync(compiler!.ImageName, cancellationToken))
        {
            if (!compiler.HasDockerLocally)
            {
                compiler.HasDockerLocally = true;

                await dbContext.SaveChangesAsync(cancellationToken);
            }

            throw new ApiException($"Образ {compiler.ImageName} уже существует.");
        }

        var result = await dockerManager.LoadDockerImageAsync(request.Stream, cancellationToken);

        if (!result)
        {
            throw new ApiException("Не удалось загрузить компилятор.");
        }

        compiler.HasDockerLocally = true;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
