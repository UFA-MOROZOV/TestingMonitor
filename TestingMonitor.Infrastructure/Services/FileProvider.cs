using System;
using System.IO;
using TestingMonitor.Application.Interfaces;

namespace TestingMonitor.Infrastructure.Services;

/// <summary>
/// Провайдер по работе с файлами.
/// </summary>
internal sealed class FileProvider : IFileProvider
{
    private readonly string _folder = "Files";

    public async Task<string> CreateWithContent(string content, Guid guid, CancellationToken cancellationToken)
    {
        try
        {
            if (!Directory.Exists(_folder))
            {
                Directory.CreateDirectory(_folder);
            }

            var path = Path.Combine(_folder, guid.ToString());

            await File.WriteAllTextAsync(path, content, cancellationToken);

            return path;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

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

    public async Task<string> GetContent(string path, CancellationToken cancellationToken)
        => await File.ReadAllTextAsync(path, cancellationToken); 

    public async Task UpdateContent(string path, string content, CancellationToken cancellationToken)
    {
        await File.WriteAllTextAsync(path, content, cancellationToken);
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
