namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Файлы Header.
/// </summary>
public sealed class HeaderFile
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Путь до файла.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Группы тестов.
    /// </summary>
    public ICollection<TestGroupToHeaderFile> TestGroups { get; set; } = [];

    /// <summary>
    /// Тесты.
    /// </summary>
    public ICollection<TestToHeaderFile> Tests { get; set; } = [];
}
