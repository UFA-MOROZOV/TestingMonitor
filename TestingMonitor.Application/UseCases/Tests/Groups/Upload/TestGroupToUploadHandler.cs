using System.IO.Compression;
using MediatR;

namespace TestingMonitor.Application.UseCases.Tests.Groups.Upload;

/// <summary>
/// Обработчик загрузки 
/// </summary>
internal sealed class TestGroupToUploadHandler : IRequestHandler<TestGroupToUploadCommand, Unit>
{
    public async Task<Unit> Handle(TestGroupToUploadCommand request, CancellationToken cancellationToken)
    {
        using var archive = new ZipArchive(request.Stream, ZipArchiveMode.Read);

        return Unit.Value;
    }
}
