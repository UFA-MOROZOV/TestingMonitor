using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.UpdateHeaderFiles;

/// <summary>
/// Команда обновления header файлов теста.
/// </summary>
public sealed class TestToUpdateHeaderFilesCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор теста.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификаторы header файлов.
    /// </summary>
    public List<Guid> HeaderIds { get; set; } = [];
}
