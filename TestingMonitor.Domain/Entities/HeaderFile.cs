namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Header file.
/// </summary>
public sealed class HeaderFile
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
    /// Path to file.
    /// </summary>
    public string Path { get; set; } = null!;

    /// <summary>
    /// Test groups.
    /// </summary>
    public ICollection<TestGroupToHeaderFile> TestGroups { get; set; } = [];

    /// <summary>
    /// Tests.
    /// </summary>
    public ICollection<TestToHeaderFile> Tests { get; set; } = [];
}
