using MediatR;
using TestingMonitor.Application.Interfaces;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.Compilers.Create;

/// <summary>
/// Обработчик загрузки докера компилятора.
/// </summary>
internal sealed class CompilerToCreateHandler(IDbContext dbContext, IDockerManager dockerManager)
    : IRequestHandler<CompilerToCreateCommand, int>
{
    /// <inheritdoc/>
    public async Task<int> Handle(CompilerToCreateCommand command, CancellationToken cancellationToken)
    {
        var compiler = new Compiler
        {
            Name = command.Name,
            Version = command.Version,
            CommandName = command.CommandName,
        };

        if (command.Tar != Stream.Null)
        {
            var result = await dockerManager.LoadDockerImageAsync(command.Tar, cancellationToken);

            if (result)
            {
                compiler.HasDockerLocally = true;
            }
        }

        await dbContext.Compilers.AddAsync(compiler, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return compiler.Id;
    }
}
