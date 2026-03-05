using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.UpdateContent;

/// <summary>
/// Command of updating header file.
/// </summary>
public sealed class HeaderFileToUpdateCommand : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Content.
    /// </summary>
    public string Content { get; set; } = null!;
}
