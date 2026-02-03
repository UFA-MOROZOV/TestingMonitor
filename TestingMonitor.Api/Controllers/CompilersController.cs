using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.DownloadDocker;
using TestingMonitor.Application.UseCases.Compilers.ExecuteCode;
using TestingMonitor.Application.UseCases.Compilers.Get;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilers")]
public sealed class CompilersController(IMediator mediator) : Controller
{
    /// <summary>
    /// Получение всех компиляторов.
    /// </summary>
    [HttpGet("/api/compilers")]
    [ProducesResponseType<List<CompilerDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompilerDto>>> GetCompilers(CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilersQuery(), cancellationToken);

    /// <summary>
    /// Выполнение кода компилятором.
    /// </summary>
    [HttpPost("/api/compilers/execute")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> ExecuteCode([FromBody] CompilerToExecuteCodeCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Загрузка докера компилятора.
    /// </summary>
    [HttpPost("/api/compilers/{id:int}/download")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DownloadDocker(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new CompilerToDownloadDockerCommand(id), cancellationToken);

        return NoContent();
    }
}