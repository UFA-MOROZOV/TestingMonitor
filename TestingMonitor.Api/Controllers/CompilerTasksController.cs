using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.CompilerTasks.Get;
using TestingMonitor.Application.UseCases.CompilerTasks.GetById;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilersTasks")]
[Authorize]
public sealed class CompilersTasksController(IMediator mediator) : Controller
{
    /// <summary>
    /// Получение всех задач компиляторов.
    /// </summary>
    [HttpGet("/api/compilersTasks")]
    [ProducesResponseType<List<CompilerTaskDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompilerTaskDto>>> GetCompilerTasks(CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilerTasksQuery(), cancellationToken);

    /// <summary>
    /// Получение задачи компилятора по идентификатору.
    /// </summary>
    [HttpGet("/api/compilersTasks/{id:guid}")]
    [ProducesResponseType<GetCompilerTaskByIdResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetCompilerTaskByIdResponse>> GetCompilerTask(Guid id, CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilerTaskByIdQuery(id), cancellationToken);
}