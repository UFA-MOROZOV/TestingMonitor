using System.IO.Compression;
using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

/// <summary>
/// Обработчик загрузки зип с тестами. 
/// </summary>
internal sealed class TestGroupToUploadHandler : IRequestHandler<TestGroupToUploadCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToUploadCommand request, CancellationToken cancellationToken)
    {
        using var archive = new ZipArchive(request.Stream, ZipArchiveMode.Read);

        Dictionary<string, Guid> groupIds = new();

        foreach (var entry in archive.Entries)
        {
            if (entry.FullName)
            {

            }
        }

        return Unit.Value;
    }
}
