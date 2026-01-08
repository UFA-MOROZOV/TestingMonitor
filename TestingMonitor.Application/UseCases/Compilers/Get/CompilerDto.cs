namespace TestingMonitor.Application.UseCases.Compilers.Get;

/// <summary>
/// Компилятор.
/// </summary>
public sealed class CompilerDto
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

    /// <summary>
    /// Есть ли локальный докер.
    /// </summary>
    public bool HasDockerLocally { get; set; }
}
