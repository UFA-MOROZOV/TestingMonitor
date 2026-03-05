using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.UpdateHeaderFiles;

/// <summary>
/// Command of updating header files list for a test group.
/// </summary>
public sealed class TestGroupToUpdateHeaderFilesCommand : IRequest<Unit>
{
    /// <summary>
    /// Id of a test group.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Header files ids.
    /// </summary>
    public List<Guid> HeaderIds { get; set; } = [];
}
