namespace TestingMonitor.Application.UseCases.CompilerTasks.Get;

/// <summary>
/// Задача.
/// </summary>
public sealed class CompilerTaskDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Выполнен ли.
    /// </summary>
    public bool IsCompleted { get; set; }
}
