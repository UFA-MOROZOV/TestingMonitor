using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Create;

/// <summary>
/// Запрос на создание теста.
/// </summary>
public sealed class TestToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Контент.
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Идентификатор группы.
    /// </summary>
    public Guid? GroupId { get; set; }
}
