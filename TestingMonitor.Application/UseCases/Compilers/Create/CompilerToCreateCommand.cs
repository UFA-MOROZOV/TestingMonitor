using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Create;

/// <summary>
/// Команда загрузки докера компилятора через tar.
/// </summary>
public sealed class CompilerToCreateCommand : IRequest<int>
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Версия.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Имя команды.
    /// </summary>
    public string CommandName { get; set; } = null!;

    /// <summary>
    /// Имя образа.
    /// </summary>
    public string ImageName { get; set; } = null!;

    /// <summary>
    /// Tar.
    /// </summary>
    public Stream Tar { get; set; } = Stream.Null;
}
