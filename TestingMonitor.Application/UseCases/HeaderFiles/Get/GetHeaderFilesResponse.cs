namespace TestingMonitor.Application.UseCases.HeaderFiles.Get;

/// <summary>
/// Результат получения тестов.
/// </summary>
public sealed class GetHeaderFilesResponse
{
    /// <summary>
    /// Подгруппы..
    /// </summary>
    public ICollection<HeaderFileDto> HeaderFiles { get; set; } = [];
}

/// <summary>
/// Объект группы.
/// </summary>
public sealed class HeaderFileDto
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