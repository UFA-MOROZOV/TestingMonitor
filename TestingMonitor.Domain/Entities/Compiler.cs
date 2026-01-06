namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Компилятор.
/// </summary>
public sealed class Compiler
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
