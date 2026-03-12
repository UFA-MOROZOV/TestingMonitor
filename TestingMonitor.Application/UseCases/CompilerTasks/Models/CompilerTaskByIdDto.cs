using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.CompilerTasks.GetById;

/// <summary>
/// Compiler task.
/// </summary>
public sealed class CompilerTaskByIdDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Used compiler.
    /// </summary>
    public CompilerDto Compiler { get; set; } = null!;

    /// <summary>
    /// Кол-во выполненных корректно тестов.
    /// </summary>
    public int SuccessfulTasksCount { get; set; }

    /// <summary>
    /// Количество задач.
    /// </summary>
    public int TasksCount { get; set; }

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