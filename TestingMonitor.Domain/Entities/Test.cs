namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Тест.
/// </summary>
public sealed class Test
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
    /// Файлы .h.
    /// </summary>
    public ICollection<string> HFiles { get; set; } = [];

    /// <summary>
    /// Путь до файла.
    /// </summary>
    public string Path { get; set; } = null!;
    
    /// <summary>
    /// Группа.
    /// </summary>
    public TestGroup? TestGroup { get; set; }

    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    public Guid? TestGroupId { get; set; } 
}
