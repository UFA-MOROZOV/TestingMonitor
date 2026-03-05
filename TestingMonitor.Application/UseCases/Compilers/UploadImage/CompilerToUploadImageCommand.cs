using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.UploadImage;

/// <summary>
/// Command of uploading a compiler image.
/// </summary>
public sealed class CompilerToUploadImageCommand : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// File.
    /// </summary>
    public Stream Stream { get; set; } = null!;
}