using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Infrastructure.Services;

/// <summary>
/// Провайдер по работе с файлами.
/// </summary>
internal sealed class FileProvider : IFileProvider
{
    private readonly string _folder = "Files";

    /// <inheritdoc/>
    public async Task DeleteFileAsync(string path, CancellationToken cancellationToken)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception ex)
        {
            return;
        }
    }

    /// <inheritdoc/>
    public async Task<string?> UploadFileAsync(Stream stream, Guid guid, CancellationToken cancellationToken)
    {
        try
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }

            var path = Path.Combine(_folder, guid.ToString());

            using var fileStream = new FileStream(path, FileMode.CreateNew);

            await stream.CopyToAsync(fileStream, cancellationToken);

            fileStream.Dispose();

            return path;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
