namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Test group and header file relation.
/// </summary>
public sealed class TestGroupToHeaderFile
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
    /// Test group id.
    /// </summary>
    public Guid TestGroupId { get; set; }

    /// <summary>
    /// Test group.
    /// </summary>
    public TestGroup? TestGroup { get; set; }
}
