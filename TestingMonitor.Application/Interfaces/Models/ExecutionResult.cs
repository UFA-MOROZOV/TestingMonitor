namespace TestingMonitor.Application.Interfaces.Models;

/// <summary>
/// Результаты исполнения.
/// </summary>
public sealed class ExecutionResult
{
    /// <summary>
    /// Вывод.
    /// </summary>
    public string Message { get; set; } = null!;

    /// <summary>
    /// Длительность.
    /// </summary>
    public TimeSpan Duration { get; set; }
}
