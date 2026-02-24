using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Create;

/// <summary>
/// Команда добавления header файла.
/// </summary>
public sealed class HeaderFileToCreateCommand : IRequest<Guid>
{
    /// <summary>
    /// Поток с файлом.
    /// </summary>
    public Stream Stream { get; set; } = null!;

    /// <summary>
    /// Имя файла.
    /// </summary>
    public string Name { get; set; } = null!;
}
