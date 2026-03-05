namespace TestingMonitor.Application.UseCases.Models;

/// <summary>
/// Unit of a group.
/// </summary>
public sealed class TestItemDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;
}