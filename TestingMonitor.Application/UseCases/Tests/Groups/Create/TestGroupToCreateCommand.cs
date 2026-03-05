using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Create;

/// <summary>
/// Command of creating a test group.
/// </summary>
public sealed class TestGroupToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Parent group Id.
    /// </summary>
    public Guid? ParentGroupId { get; set; }
}
