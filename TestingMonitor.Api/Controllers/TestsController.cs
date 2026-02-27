using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.HeaderFiles.Create;
using TestingMonitor.Application.UseCases.HeaderFiles.Delete;
using TestingMonitor.Application.UseCases.HeaderFiles.Get;
using TestingMonitor.Application.UseCases.Tests.Create;
using TestingMonitor.Application.UseCases.Tests.Delete;
using TestingMonitor.Application.UseCases.Tests.Get;
using TestingMonitor.Application.UseCases.Tests.GetContent;
using TestingMonitor.Application.UseCases.Tests.Groups.Create;
using TestingMonitor.Application.UseCases.Tests.Groups.Delete;
using TestingMonitor.Application.UseCases.Tests.Groups.UpdateHeaderFiles;
using TestingMonitor.Application.UseCases.Tests.Groups.Upload;
using TestingMonitor.Application.UseCases.Tests.UpdateContent;
using TestingMonitor.Application.UseCases.Tests.UpdateHeaderFiles;

namespace TestingMonitor.Api.Controllers;

[Route("/api/tests")]
[Authorize]
public sealed class TestsController(IMediator mediator) : Controller
{
    /// <summary>
    /// Получние тестов.
    /// </summary>
    [HttpGet("/api/tests")]
    [ProducesResponseType<GetTestsResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTestsResponse>> GetTests(GetTestsQuery query,
        CancellationToken cancellationToken)
        => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Создание теста через файл.
    /// </summary>
    [HttpPost("/api/tests/upload")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateTest(IFormFile file, [FromQuery] Guid? groupId,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new TestToUploadCommand
        {
            Stream = stream,
            GroupId = groupId,
            Name = file.FileName,
        };

        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение содержимого.
    /// </summary>
    [HttpGet("/api/tests/{id:guid}/content")]
    [ProducesResponseType<GetTestContentByIdResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTestContentByIdResponse>> GetContent(Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTestContentByIdQuery(id), cancellationToken);

    /// <summary>
    /// Обновление теста.
    /// </summary>
    [HttpPut("/api/tests")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateTest(TestToUpdateCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Удаление теста.
    /// </summary>
    [HttpDelete("/api/tests/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteTest(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new TestToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Обновление header файлов теста.
    /// </summary>
    [HttpPut("/api/tests/headerFiles")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateHeaderFiles(TestToUpdateHeaderFilesCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    #region Groups

    /// <summary>
    /// Создание группы тестов.
    /// </summary>
    [HttpPost("/api/testGroup")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Guid>> CreateTestGroup(TestGroupToCreateCommand command,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Удаление группы тестов.
    /// </summary>
    [HttpDelete("/api/testGroup/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteTestGroup(Guid id,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new TestGroupToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Загрузка группы тестов через зип.
    /// </summary>
    [HttpPost("/api/testsgroups/upload")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UploadTests(IFormFile file, [FromQuery] Guid? groupId,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new TestGroupToUploadCommand
        {
            Stream = stream,
            GroupId = groupId,
        };

        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Обновление header файлов группы тестов.
    /// </summary>
    [HttpPut("/api/testsgroups/headerFiles")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateGroupHeaderFiles(TestGroupToUpdateHeaderFilesCommand command,
        CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    #endregion

    #region HeaderFiles

    /// <summary>
    /// Получение header файлов.
    /// </summary>
    [HttpGet("/api/tests/headerFiles")]
    [ProducesResponseType<GetHeaderFilesResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetHeaderFilesResponse>> CreateHeaderFile(GetHeaderFilesQuery query,
        CancellationToken cancellationToken)
        => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Создание header файла.
    /// </summary>
    [HttpPost("/api/tests/headerFiles")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateHeaderFile(IFormFile file,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new HeaderFileToCreateCommand
        {
            Stream = stream,
            Name = file.FileName,
        };

        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление header файла.
    /// </summary>
    [HttpDelete("/api/tests/headerFiles/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteHeaderFile(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new HeaderFileToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    #endregion
}