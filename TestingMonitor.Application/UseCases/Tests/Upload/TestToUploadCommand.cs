using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Upload;

/// <summary>
/// Command of uploading a test.
/// </summary>
public sealed class TestToUploadCommand : IRequest<Guid>
{
    /// <summary>
    /// File stream
    /// </summary>
    public Stream Stream { get; set; } = null!;

    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Group Id.
    /// </summary>
    public Guid? GroupId { get; set; }
}
