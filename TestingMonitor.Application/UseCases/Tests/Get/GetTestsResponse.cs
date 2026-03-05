using TestingMonitor.Application.UseCases.Models;

namespace TestingMonitor.Application.UseCases.Tests.Get;

/// <summary>
/// Result of getting tests.
/// </summary>
public sealed class GetTestsResponse
{
    /// <summary>
    /// Subgroups.
    /// </summary>
    public ICollection<TestItemDto> SubGroups { get; set; } = [];

    /// <summary>
    /// Tests.
    /// </summary>
    public ICollection<TestItemDto> Tests { get; set; } = [];
}

