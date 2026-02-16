using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.Create;
using TestingMonitor.Application.UseCases.Compilers.DownloadDocker;
using TestingMonitor.Application.UseCases.Compilers.ExecuteCode;
using TestingMonitor.Application.UseCases.Compilers.Get;
using TestingMonitor.Application.UseCases.Compilers.Update;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilers")]
[Authorize]
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
    /// Создание компилятора.
    /// </summary>
    [HttpPost("/api/compilers")]
    [ProducesResponseType<int>(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CreateCompiler(string name, string version, string commandName, IFormFile? formFile, CancellationToken cancellationToken)
    {
        var command = new CompilerToCreateCommand
        {
            Name = name,
            Version = version,
            CommandName = commandName,
        };

        if (formFile != null)
        {
            using var stream = formFile.OpenReadStream();

            command.Tar = stream;
        }


        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Обновление данных компилятора.
    /// </summary>
    [HttpPut("/api/compilers")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateCompiler(CompilerToUpdateCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

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

    /// <summary>
    /// Загрузка докера компилятора.
    /// </summary>
    [HttpPost("/api/compilers/download")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DownloadDockerTar(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new CompilerToDownloadDockerCommand(id), cancellationToken);

        return NoContent();
    }
}