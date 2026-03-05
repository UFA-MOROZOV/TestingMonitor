using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.DownloadDocker;

/// <summary>
/// Command of a compiler image downloading from repo.
/// </summary>
public sealed class CompilerToDownloadDockerCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; } = id;
}
