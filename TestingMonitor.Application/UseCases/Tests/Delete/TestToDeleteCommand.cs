using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Delete;

/// <summary>
/// Command of a test deletion.
/// </summary>
public sealed class TestToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = id;
}
