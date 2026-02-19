using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Api.Models;
using TestingMonitor.Application.UseCases.Compilers.Create;
using TestingMonitor.Application.UseCases.Compilers.Delete;
using TestingMonitor.Application.UseCases.Compilers.DeleteImage;
using TestingMonitor.Application.UseCases.Compilers.DownloadDocker;
using TestingMonitor.Application.UseCases.Compilers.ExecuteCode;
using TestingMonitor.Application.UseCases.Compilers.Get;
using TestingMonitor.Application.UseCases.Compilers.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    public async Task<ActionResult<int>> CreateCompiler([FromForm] CompilerModel compilerModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            // Log errors or return BadRequest with details
            return BadRequest(ModelState);
        }

        var command = new CompilerToCreateCommand
        {
            Name = compilerModel.Name,
            Version = compilerModel.Version,
            CommandName = compilerModel.CommandName,
            ImageName = $"{compilerModel.Name}:{compilerModel.Version}",
        };

        Stream? stream = Stream.Null;

        if (compilerModel.File != null)
        {
            stream = compilerModel.File.OpenReadStream();

            command.Tar = stream;
        }

        var response = await mediator.Send(command, cancellationToken);

        if (stream != Stream.Null)
        {
            stream.Dispose();
        }

        return response;
    }

    /// <summary>
    /// Обновление данных компилятора.
    /// </summary>
    [HttpPut("/api/compilers")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateCompiler([FromBody] CompilerToUpdateCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Выполнение кода компилятором.
    /// </summary>
    [HttpPost("/api/compilers/execute")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    public async Task<ActionResult<string>> ExecuteCode([FromBody] CompilerToExecuteCodeCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Удаление компилятора.
    /// </summary>
    [HttpPost("/api/compilers/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteCompiler(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new CompilerToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Удаление образа компилятора.
    /// </summary>
    [HttpPost("/api/compilers/{id:int}/image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteImage(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new CompilerToDeleteImageCommand(id), cancellationToken);

        return NoContent();
    }

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
    /// Загрузка образа компилятора через файл.
    /// </summary>
    [HttpPost("/api/compilers/{id:int}/uploadFile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UploadDocker(int id, [FromForm] FormFile imageFile, CancellationToken cancellationToken)
    {
        var stream = imageFile.OpenReadStream();

        await mediator.Send(new CompilerToDownloadDockerCommand(id), cancellationToken);

        stream.Dispose();

        return NoContent();
    }
}