using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.UpdateHeaderFiles;

/// <summary>
/// Команда обновления header файлов группы тестов.
/// </summary>
public sealed class TestGroupToUpdateHeaderFilesCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор группы тестов.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификаторы header файлов.
    /// </summary>
    public List<Guid> HeaderIds { get; set; } = [];
}
