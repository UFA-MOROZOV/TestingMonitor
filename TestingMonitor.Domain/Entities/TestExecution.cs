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
    /// Duration.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Is test execution a success.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Output.
    /// </summary>
    public string? Output { get; set; }
}
