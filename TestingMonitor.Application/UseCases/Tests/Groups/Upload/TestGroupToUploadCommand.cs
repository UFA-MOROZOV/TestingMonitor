using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

/// <summary>
/// Команда загрузки групы тестов через зип файл.
/// </summary>
public sealed class TestGroupToUploadCommand : IRequest<Unit>
{
    /// <summary>
    /// Поток с зип файлом.
    /// </summary>
    public Stream Stream { get; set; } = null!;
}
