using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

/// <summary>
/// Command of uploading a test group.
/// </summary>
public sealed class TestGroupToUploadCommand : IRequest<Unit>
{
    /// <summary>
    /// File stream.
    /// </summary>
    public Stream Stream { get; set; } = null!;

    /// <summary>
    /// Group Id.
    /// </summary>
    public Guid? GroupId { get; set; }
}
