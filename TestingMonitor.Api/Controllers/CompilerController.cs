using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.ExecuteCode;
using TestingMonitor.Application.UseCases.Compilers.Get;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilers")]
public sealed class CompilerController(IMediator mediator) : Controller
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
}