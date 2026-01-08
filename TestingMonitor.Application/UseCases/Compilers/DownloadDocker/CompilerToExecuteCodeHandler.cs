using MediatR;
using Microsoft.EntityFrameworkCore;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Application.UseCases.Compilers.DownloadDocker;

/// <summary>
/// Обработчик загрузки докера компилятора.
/// </summary>
internal sealed class CompilerToDownloadDockerHandler (IDbContext dbContext, IDockerExecutor dockerExecutor)
    : IRequestHandler<CompilerToDownloadDockerCommand, Unit>
{
    /// <inheritdoc/>
    public async Task<Unit> Handle(CompilerToDownloadDockerCommand command, CancellationToken cancellationToken)
    {
        var compiler = await dbContext.Compilers
            .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (compiler == null || compiler.HasDockerLocally)
        {
            return Unit.Value; 
        }

        var progress = new Progress<string>(message =>
        {
            Console.WriteLine($"Download progress: {message}");
        });

        var output = await dockerExecutor.DownloadCompilerAsync(compiler, progress, cancellationToken);

        compiler.HasDockerLocally = true;

        return Unit.Value;
    }
}
