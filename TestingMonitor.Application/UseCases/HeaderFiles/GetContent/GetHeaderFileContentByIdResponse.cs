using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.GetContent;

/// <summary>
/// Результат получения содержимого header файла.
/// </summary>
public sealed class GetHeaderFileContentByIdResponse: IRequest<Unit>
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
