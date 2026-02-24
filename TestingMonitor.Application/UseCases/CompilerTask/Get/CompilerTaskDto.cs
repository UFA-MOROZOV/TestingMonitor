namespace TestingMonitor.Application.UseCases.TaskExecutions.Get;

/// <summary>
/// Задача.
/// </summary>
public sealed class CompilerTaskDto
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
    /// Выполнен ли.
    /// </summary>
    public bool IsCompleted { get; set; }
}
