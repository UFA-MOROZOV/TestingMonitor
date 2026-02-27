using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.DownloadDocker;

/// <summary>
/// Команда загрузки докера компилятора с репозиториев.
/// </summary>
public sealed class CompilerToDownloadDockerCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор компилятора.
    /// </summary>
    public int Id { get; set; } = id;
}
