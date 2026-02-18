namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Связь тест-header.
/// </summary>
public sealed class TestToHeaderFile
{
    /// <summary>
    /// Идентификатор header.
    /// </summary>
    public Guid HeaderId { get; set; }

    /// <summary>
    /// Header.
    /// </summary>
    public HeaderFile? HeaderFile { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid TestId { get; set; }

    /// <summary>
    /// Тест.
    /// </summary>
    public Test? Test { get; set; }
}
