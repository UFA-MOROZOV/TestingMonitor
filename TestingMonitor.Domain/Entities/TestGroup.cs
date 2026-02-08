namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Группа тестов.
/// </summary>
public sealed class TestGroup
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя группы.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Группа родительская.
    /// </summary>
    public Guid? ParentGroupId { get; set; }

    /// <summary>
    /// Группа родительская.
    /// </summary>
    public TestGroup? ParentGroup { get; set; }

    /// <summary>
    /// Тест.
    /// </summary>
    public ICollection<Test> Tests { get; set; } = [];

    /// <summary>
    /// Подгруппы.
    /// </summary>
    public ICollection<TestGroup> SubGroups { get; set; } = [];
}
