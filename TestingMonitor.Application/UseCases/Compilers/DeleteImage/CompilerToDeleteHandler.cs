using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.DeleteImage;

/// <summary>
/// Обработчик удаления образа компилятора.
/// </summary>
internal sealed class CompilerToDeleteImageHandler (IDockerManager dockerManager, IDbContext dbContext) 
    : IRequestHandler<CompilerToDeleteImageCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToDeleteImageCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("Компилятор с такими данными не существует.");
        }

        if (!compiler.HasDockerLocally)
        {
            return Unit.Value;
        }

        if (compiler.HasDockerLocally && await dockerManager.ImageExistsAsync(compiler.ImageName, cancellationToken))
        {
            if (!await dockerManager.DeleteDockerImageAsync(compiler, cancellationToken))
            {
                throw new ApiException($"Не получилось удалить образ.");
            }
        }

        compiler.HasDockerLocally = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
