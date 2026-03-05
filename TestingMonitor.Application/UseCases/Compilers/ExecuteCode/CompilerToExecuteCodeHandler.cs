using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Enums;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteCode;

internal sealed class CompilerToExecuteCodeHandler(IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilerToExecuteCodeCommand, string>
{
    /// <inheritdoc/>
    public async Task<string> Handle(CompilerToExecuteCodeCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null)
        {
            ErrorCode.CompilerNotFound.Throw();
        }

        var output = await dockerManager.ExecuteCodeAsync(compiler!, Guid.NewGuid(), command.Code, cancellationToken);

        return output.Message;
    }
}
