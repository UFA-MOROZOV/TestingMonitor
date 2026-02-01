using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Команда добавления теста.
/// </summary>
public sealed class TestToCreateCommand : IRequest<Unit>
{
    /// <summary>
    /// Поток с файлом.
    /// </summary>
    public Stream Stream { get; set; } = null!;

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    public Guid GroupId { get; set; }
}
