namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Execution stats of a test.
/// </summary>
public sealed class TestExecution
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Compiler task Id.
    /// </summary>
    public Guid CompilerTaskId { get; set; }

    /// <summary>
    /// Compiler task.
    /// </summary>
    public CompilerTask? CompilerTask { get; set; }

    /// <summary>
    /// Test Id.
    /// </summary>
    public Guid? TestId { get; set; }

    /// <summary>
    /// Test.
    /// </summary>
    public Test? Test { get; set; }

    /// <summary>
    /// Duration of a compilation process.
    /// </summary>
    public TimeSpan? CompileDuration { get; set; }

    /// <summary>
    /// Duration of an execution process.
    /// </summary>
    public TimeSpan? RunDuration { get; set; }

    /// <summary>
    /// Output of a compiler.
    /// </summary>
    public string CompilerOutput { get; set; } = string.Empty;

    /// <summary>
    /// Output of program execution.
    /// </summary>
    public string ProgramOutput { get; set; } = string.Empty;

    /// <summary>
    /// Exit code of a compiler.
    /// </summary>
    public int? CompilerExitCode { get; set; }

    /// <summary>
    /// Exit code of a compiled file.
    /// </summary>
    public int? ProgramExitCode { get; set; }

    /// <summary>
    /// Is compilation succeeded
    /// </summary>
    public bool? CompilationSucceeded { get; set; }
}
