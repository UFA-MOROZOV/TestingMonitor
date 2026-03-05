using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.UpdateContent;

/// <summary>
/// Command of updating a test.
/// </summary>
public sealed class TestToUpdateCommand : IRequest<Unit>
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
    /// Content.
    /// </summary>
    public string Content { get; set; } = null!;
}
