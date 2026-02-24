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
    /// Тест.
    /// </summary>
    public TestItemDto? Test { get; set; }

    /// <summary>
    /// Группа тестов.
    /// </summary>
    public TestItemDto? TestGroup { get; set; }

    /// <summary>
    /// Выполненные тесты.
    /// </summary>
    public ICollection<TaskExecutionDto> TestsExecuted { get; set; } = [];
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