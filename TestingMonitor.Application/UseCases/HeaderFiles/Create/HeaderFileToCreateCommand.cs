using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Create;

/// <summary>
/// Command of a header file creation.
/// </summary>
public sealed class HeaderFileToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Content of a files.
    /// </summary>
    public string Content { get; set; } = null!;
}
