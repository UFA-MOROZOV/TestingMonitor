namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Test group.
/// </summary>
public sealed class TestGroup
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Parent group.
    /// </summary>
    public Guid? ParentGroupId { get; set; }

    /// <summary>
    /// Parent group id.
    /// </summary>
    public TestGroup? ParentGroup { get; set; }

    /// <summary>
    /// Tests.
    /// </summary>
    public ICollection<Test> Tests { get; set; } = [];

    /// <summary>
    /// Header files to include.
    /// </summary>
    public ICollection<TestGroupToHeaderFile> HeaderFiles { get; set; } = [];

    /// <summary>
    /// Subgroups.
    /// </summary>
    public ICollection<TestGroup> SubGroups { get; set; } = [];
}
