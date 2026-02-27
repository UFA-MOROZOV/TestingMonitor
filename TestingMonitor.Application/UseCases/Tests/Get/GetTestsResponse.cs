using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.Tests.Get;

/// <summary>
/// Результат получения тестов.
/// </summary>
public sealed class GetTestsResponse
{
    /// <summary>
    /// Подгруппы..
    /// </summary>
    public ICollection<TestItemDto> SubGroups { get; set; } = [];

    /// <summary>
    /// Тесты.
    /// </summary>
    public ICollection<TestItemDto> Tests { get; set; } = [];
}

