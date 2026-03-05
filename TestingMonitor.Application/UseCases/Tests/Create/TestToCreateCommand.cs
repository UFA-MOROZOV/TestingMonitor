using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Command of creating a test.
/// </summary>
public sealed class TestToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Group Id.
    /// </summary>
    public Guid? GroupId { get; set; }
}
