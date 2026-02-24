namespace TestingMonitor.Application.UseCases.Models;

/// <summary>
/// Объект группы.
/// </summary>
public sealed class TestItemDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;
}