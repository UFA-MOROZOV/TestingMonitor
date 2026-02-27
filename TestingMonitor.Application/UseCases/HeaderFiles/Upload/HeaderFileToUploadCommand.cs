using MediatR;

namespace TestingMonitor.Application.UseCases.HeaderFiles.Upload;

/// <summary>
/// Команда загрузки header файла.
/// </summary>
public sealed class HeaderFileToUploadCommand : IRequest<Guid>
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
