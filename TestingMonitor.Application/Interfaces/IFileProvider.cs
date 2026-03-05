namespace TestingMonitor.Application.Interfaces;

/// <summary>
/// Провайдер файлов.
/// </summary>
public interface IFileProvider
{
    /// <summary>
    /// Загрузка файлов.
    /// </summary>
    public Task<string?> UploadFileAsync(Stream stream, Guid guid, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление файлов.
    /// </summary>
    public Task DeleteFileAsync(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Получение контента файла по пути.
    /// </summary>
    public Task<string> GetContent(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление содержимого файла.
    /// </summary>
    public Task UpdateContent(string path, string content, CancellationToken cancellationToken);

    /// <summary>
    /// Создание файла с содержимым.
    /// </summary>
    public Task<string> CreateWithContent(string content, Guid guid, CancellationToken cancellationToken);
}
