using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.CompilerTasks.GetById;

/// <summary>
/// Compiler task.
/// </summary>
public sealed class GetCompilerTaskByIdResponse
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Used compiler.
    /// </summary>
    public CompilerDto Compiler { get; set; } = null!;

    /// <summary>
    /// Date of creation.
    /// </summary>
    public DateTime DateOfCreation { get; set; }

    /// <summary>
    /// Date of beginning of execution.
    /// </summary>
    public DateTime? DateOfStart { get; set; }

    /// <summary>
    /// Date of completion.
    /// </summary>
    public DateTime? DateOfCompletion { get; set; }

    /// <summary>
    /// Test.
    /// </summary>
    public TestItemDto? Test { get; set; }

    /// <summary>
    /// Test group.
    /// </summary>
    public TestItemDto? TestGroup { get; set; }

    /// <summary>
    /// Executed tests.
    /// </summary>
    public ICollection<TestExecutionDto> TestsExecuted { get; set; } = [];
}

public sealed class TestExecutionDto
{
    /// <summary>
    /// Test.
    /// </summary>
    public TestItemDto? Test { get; set; }

    /// <summary>
    /// Duration of a test.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Is it successful.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Output string.
    /// </summary>
    public string? Output { get; set; }
}