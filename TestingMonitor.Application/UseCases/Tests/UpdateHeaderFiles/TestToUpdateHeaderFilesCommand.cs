using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.UpdateHeaderFiles;

/// <summary>
/// Command of updating header files list for a test.
/// </summary>
public sealed class TestToUpdateHeaderFilesCommand : IRequest<Unit>
{
    /// <summary>
    /// Test Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Header files ids.
    /// </summary>
    public List<Guid> HeaderIds { get; set; } = [];
}
