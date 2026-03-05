namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Test and header file relation.
/// </summary>
public sealed class TestToHeaderFile
{
    /// <summary>
    /// Header Id.
    /// </summary>
    public Guid HeaderId { get; set; }

    /// <summary>
    /// Header.
    /// </summary>
    public HeaderFile? HeaderFile { get; set; }

    /// <summary>
    /// Test Id.
    /// </summary>
    public Guid TestId { get; set; }

    /// <summary>
    /// Test.
    /// </summary>
    public Test? Test { get; set; }
}
