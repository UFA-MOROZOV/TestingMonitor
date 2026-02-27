using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.GetContent;

/// <summary>
/// Результат получения содержимого файла.
/// </summary>
public sealed class GetTestContentByIdResponse: IRequest<Unit>
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
