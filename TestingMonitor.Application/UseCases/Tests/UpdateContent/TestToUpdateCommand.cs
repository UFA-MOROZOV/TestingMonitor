using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.UpdateContent;

/// <summary>
/// Запрос на обновление данных теста.
/// </summary>
public sealed class TestToUpdateCommand : IRequest<Unit>
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Контент.
    /// </summary>
    public string Content { get; set; } = null!;
}
