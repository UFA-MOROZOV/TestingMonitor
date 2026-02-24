namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Результат выполнения теста.
/// </summary>
public sealed class TestExecution
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор на выполнение.
    /// </summary>
    public Guid ExecutionTaskId { get; set; }

    /// <summary>
    /// Задача на выполнение.
    /// </summary>
    public ExecutionTask? ExecutionTask { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid? TestId { get; set; }

    /// <summary>
    /// Тест.
    /// </summary>
    public Test? Test { get; set; }

    /// <summary>
    /// Время выполнения в секундах.
    /// </summary>
    public int DurationInSeconds { get; set; }

    /// <summary>
    /// Успешно ли.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// Сообщение об ошибке.
    /// </summary>
    public string? ErrorMessage { get; set; }
}
