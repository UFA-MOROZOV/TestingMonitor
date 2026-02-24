namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Связь группа-header.
/// </summary>
public sealed class TestGroupToHeaderFile
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
    public Guid TestGroupId { get; set; }

    /// <summary>
    /// Группа.
    /// </summary>
    public TestGroup? TestGroup { get; set; }
}
