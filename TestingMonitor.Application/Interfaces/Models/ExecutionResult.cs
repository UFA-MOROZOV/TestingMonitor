namespace TestingMonitor.Application.Interfaces.Models;

/// <summary>
/// Result of a test execution.
/// </summary>
public sealed class ExecutionResult
{
    public TimeSpan? CompileDuration { get; set; }
    public TimeSpan? RunDuration { get; set; }
    public string CompilerOutput { get; set; } = string.Empty;
    public string ProgramOutput { get; set; } = string.Empty;
    public int? CompilerExitCode { get; set; }
    public int? ProgramExitCode { get; set; }
    public bool? CompilationSucceeded { get; set; }
}
