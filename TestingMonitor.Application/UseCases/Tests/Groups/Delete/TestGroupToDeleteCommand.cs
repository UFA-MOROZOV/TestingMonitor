using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Delete;

/// <summary>
/// Command of a test group deletion.
/// </summary>
public sealed class TestGroupToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = id;
}
