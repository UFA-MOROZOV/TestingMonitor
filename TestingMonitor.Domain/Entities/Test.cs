namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Test.
/// </summary>
public sealed class Test
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
    /// Header files to include.
    /// </summary>
    public ICollection<TestToHeaderFile> HeaderFiles { get; set; } = [];

    /// <summary>
    /// Path to file.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Group.
    /// </summary>
    public TestGroup? TestGroup { get; set; }

    /// <summary>
    /// Group Id.
    /// </summary>
    public Guid? TestGroupId { get; set; }
}