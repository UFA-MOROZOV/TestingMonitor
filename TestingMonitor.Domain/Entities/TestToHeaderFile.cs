namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Связь тест-header.
/// </summary>
public sealed class TestToHeaderFile
{
    /// <summary>
    /// Идентификатор header.
    /// </summary>
    public int HeaderId { get; set; }

    /// <summary>
    /// Header.
    /// </summary>
    public HeaderFile? HeaderFile { get; set; }

    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public int TestId { get; set; }

    /// <summary>
    /// Тест.
    /// </summary>
    public Test? Test { get; set; }
}
