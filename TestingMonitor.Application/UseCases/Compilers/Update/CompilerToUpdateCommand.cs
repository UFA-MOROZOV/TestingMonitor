using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Update;

/// <summary>
/// Команда загрузки докера компилятора через tar.
/// </summary>
public sealed class CompilerToUpdateCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; set; }

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
}
