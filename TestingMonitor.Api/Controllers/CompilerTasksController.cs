using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.CompilerTasks.ExportById;
using TestingMonitor.Application.UseCases.CompilerTasks.Get;
using TestingMonitor.Application.UseCases.CompilerTasks.GetById;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilersTasks")]
[Authorize]
public sealed class CompilersTasksController(IMediator mediator) : Controller
{
    /// <summary>
    /// Get all compiler tasks.
    /// </summary>
    [HttpGet("/api/compilersTasks")]
    [ProducesResponseType<List<CompilerTaskDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompilerTaskDto>>> GetCompilerTasks(CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilerTasksQuery(), cancellationToken);

    /// <summary>
    /// Get a compiler task.
    /// </summary>
    [HttpGet("/api/compilersTasks/{id:guid}")]
    [ProducesResponseType<CompilerTaskDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult<CompilerTaskByIdDto>> GetCompilerTask(Guid id, CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilerTaskByIdQuery(id), cancellationToken);

    /// <summary>
    /// Get a compiler task.
    /// </summary>
    [HttpGet("/api/compilersTasks/{id:guid}/export")]
    [ProducesResponseType<CompilerTaskByIdDto>(StatusCodes.Status200OK)]
    public async Task<ActionResult> ExportCompilerTask(Guid id, CancellationToken cancellationToken)
    {
        var stream = await mediator.Send(new CompilerTaskToExportCommand(id), cancellationToken);

        return File(stream, "application/vnd.ms-excel", $"task-{id}.xlsx");
    }
}