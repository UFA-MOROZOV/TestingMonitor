using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.Interfaces;

public interface IDockerManager
{
    public Task<string> ExecuteCodeAsync(Compiler compiler, string code, CancellationToken cancellationToken);

    public Task<bool> DownloadCompilerAsync(Compiler compiler, CancellationToken cancellationToken);

    public Task<bool> LoadDockerImageAsync(Stream tarStream, CancellationToken cancellationToken);

    public Task<bool> DeleteDockerImageAsync(Compiler compiler, CancellationToken cancellationToken);
}
