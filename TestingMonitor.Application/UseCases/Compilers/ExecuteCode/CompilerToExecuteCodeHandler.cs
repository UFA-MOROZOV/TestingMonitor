using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Exceptions;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteCode;

/// <summary>
/// Обработчик выполнения кода компилятором.
/// </summary>
internal sealed class CompilerToExecuteCodeHandler (IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilerToExecuteCodeCommand, string>
{
    /// <inheritdoc/>
    public async Task<string> Handle(CompilerToExecuteCodeCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            throw new ApiException("No compiler found"); 
        }

        var output = await dockerManager.ExecuteCodeAsync(compiler, command.Code, cancellationToken);

        return output;
    }
}
