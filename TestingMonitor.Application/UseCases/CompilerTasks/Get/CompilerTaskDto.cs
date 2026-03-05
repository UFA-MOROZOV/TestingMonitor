namespace TestingMonitor.Application.UseCases.CompilerTasks.Get;

/// <summary>
/// Execution task of a compiler.
/// </summary>
public sealed class CompilerTaskDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Is it completed?
    /// </summary>
    public bool IsCompleted { get; set; }
}
