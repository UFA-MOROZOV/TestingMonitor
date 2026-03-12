using MediatR;

namespace TestingMonitor.Application.UseCases.CompilerTasks.ExportById;

/// <summary>
/// Command of exporting a compiler task.
/// </summary>
public sealed class CompilerTaskToExportCommand(Guid id) : IRequest<byte[]>
{
    public Guid Id { get; set; } = id;
}