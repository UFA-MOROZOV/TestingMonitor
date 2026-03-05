namespace TestingMonitor.Application.Interfaces;

/// <summary>
/// Provider for working with files.
/// </summary>
public interface IFileProvider
{
    /// <summary>
    /// Upload a file.
    /// </summary>
    public Task<string?> UploadFileAsync(Stream stream, Guid guid, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a file.
    /// </summary>
    public Task DeleteFileAsync(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Get content of a file at a specified path.
    /// </summary>
    public Task<string> GetContent(string path, CancellationToken cancellationToken);

    /// <summary>
    /// Update a files content.
    /// </summary>
    public Task UpdateContent(string path, string content, CancellationToken cancellationToken);

    /// <summary>
    /// Create a file from some text.
    /// </summary>
    public Task<string> CreateWithContent(string content, Guid guid, CancellationToken cancellationToken);
}
