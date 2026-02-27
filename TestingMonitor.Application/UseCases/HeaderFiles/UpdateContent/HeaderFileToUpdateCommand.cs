using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.UpdateContent;

/// <summary>
/// Запрос на обновление данных header файла.
/// </summary>
public sealed class HeaderFileToUpdateCommand : IRequest<Unit>
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
