namespace TestingMonitor.Application.UseCases.Tests.Get;

/// <summary>
/// Результат получения тестов.
/// </summary>
public sealed class GetTestsResponse
{
    /// <summary>
    /// Подгруппы..
    /// </summary>
    public ICollection<ItemDto> SubGroups { get; set; } = [];

    /// <summary>
    /// Тесты.
    /// </summary>
    public ICollection<ItemDto> Tests { get; set; } = [];
}

/// <summary>
/// Объект группы.
/// </summary>
public class ItemDto
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