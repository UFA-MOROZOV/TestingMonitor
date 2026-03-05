using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Upload;

/// <summary>
/// Command of uploading a header file
/// </summary>
public sealed class HeaderFileToUploadCommand : IRequest<Guid>
{
    /// <summary>
    /// File stream.
    /// </summary>
    public Stream Stream { get; set; } = null!;

    /// <summary>
    /// File name.
    /// </summary>
    public string Name { get; set; } = null!;
}
