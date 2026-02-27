using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Create;

/// <summary>
/// Запрос на создание header файла.
/// </summary>
public sealed class HeaderFileToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Контент.
    /// </summary>
    public string Content { get; set; } = null!;
}
