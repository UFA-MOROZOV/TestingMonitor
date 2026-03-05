using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Create;

/// <summary>
/// Command of a compiler creation.
/// </summary>
public sealed class CompilerToCreateCommand : IRequest<int>
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Version.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Command to execute.
    /// </summary>
    public string CommandName { get; set; } = null!;

    /// <summary>
    /// Image name.
    /// </summary>
    public string ImageName { get; set; } = null!;

    /// <summary>
    /// Tar.
    /// </summary>
    public Stream Tar { get; set; } = Stream.Null;
}
