using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

/// <summary>
/// Обработчик удаления компилятора.
/// </summary>
internal sealed class CompilerToDeleteHandler (IDockerManager dockerManager, IDbContext dbContext) 
    : IRequestHandler<CompilerToDeleteCommand, Unit>
{
    public async Task<Unit> Handle(CompilerToDeleteCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("Компилятор с такими данными не существует.");
        }

        if (compiler.HasDockerLocally && await dockerManager.ImageExistsAsync(compiler.ImageName, cancellationToken))
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
