using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Delete;

/// <summary>
/// Команда удаления header файла.
/// </summary>
public sealed class HeaderFileToDeleteCommand(Guid id) : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; } = id;
}
