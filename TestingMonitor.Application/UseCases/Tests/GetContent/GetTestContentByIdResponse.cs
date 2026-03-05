using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.GetContent;

/// <summary>
/// Test content.
/// </summary>
public sealed class GetTestContentByIdResponse : IRequest<Unit>
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

    /// <summary>
    /// Group Id.
    /// </summary>
    public Guid? TestGroupId { get; set; }
}
