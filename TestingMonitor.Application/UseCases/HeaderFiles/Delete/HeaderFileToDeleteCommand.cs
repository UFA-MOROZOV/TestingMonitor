using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Delete;

/// <summary>
/// Comand of a header file deletion.
/// </summary>
public sealed class HeaderFileToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = id;
}
