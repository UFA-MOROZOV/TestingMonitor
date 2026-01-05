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
}
