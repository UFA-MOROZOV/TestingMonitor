using TestingMonitor.Application.UseCases.Models;
using TestingMonitor.Domain.Entities;

namespace TestingMonitor.Application.UseCases.CompilerTasks.GetById;

/// <summary>
/// Результат получения задачи компилятора.
/// </summary>
public sealed class GetCompilerTaskByIdResponse
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор компилятора.
    /// </summary>
    public int CompilerId { get; set; }

    /// <summary>
    /// Компилятор.
    /// </summary>
    public CompilerDto Compiler { get; set; } = null!;

    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime DateOfCreation { get; set; }

    /// <summary>
    /// Дата начала.
    /// </summary>
    public DateTime? DateOfStart { get; set; }

    /// <summary>
    /// Дата окончания.
    /// </summary>
    public DateTime? DateOfCompletion { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid? TestId { get; set; }

    /// <summary>
    /// Тест.
    /// </summary>
    public Test? Test { get; set; }

    /// <summary>
    /// Идентификатор группы теста.
    /// </summary>
    public Guid? TestGroupId { get; set; }

    /// <summary>
    /// Группа тестов.
    /// </summary>
    public TestGroup? TestGroup { get; set; }

    /// <summary>
    /// Выполненные тесты.
    /// </summary>
    public ICollection<TestExecution> TestsExecuted { get; set; } = [];
}

public sealed class TaskExecutionDto
{
    /// <summary>
    /// Тест.
    /// </summary>
    public TestItemDto? Test { get; set; }

    /// <summary>
    /// Время выполнения.
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Успешно ли.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string? ErrorMessage { get; set; }
}