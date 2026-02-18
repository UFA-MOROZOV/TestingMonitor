using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.HeaderFiles.Create;
using TestingMonitor.Application.UseCases.HeaderFiles.Delete;
using TestingMonitor.Application.UseCases.HeaderFiles.Get;
using TestingMonitor.Application.UseCases.Tests.Create;
using TestingMonitor.Application.UseCases.Tests.Delete;
using TestingMonitor.Application.UseCases.Tests.Get;
using TestingMonitor.Application.UseCases.Tests.Groups.Create;
using TestingMonitor.Application.UseCases.Tests.Groups.Delete;
using TestingMonitor.Application.UseCases.Tests.Groups.Upload;

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
    /// Создание теста.
    /// </summary>
    [HttpPost("/api/tests")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateTest(IFormFile file, [FromQuery] Guid? groupId,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new TestToCreateCommand
        {
            Stream = stream,
            GroupId = groupId,
            Name = file.Name,
        };

        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление теста.
    /// </summary>
    [HttpDelete("/api/tests/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteTest(Guid id, CancellationToken cancellationToken)
        => await mediator.Send(new TestToDeleteCommand(id), cancellationToken);

    #region Groups

    /// <summary>
    /// Создание группы тестов.
    /// </summary>
    [HttpPost("/api/testGroup")]
    [ProducesResponseType<Guid>(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Guid>> CreateTestGroup(TestGroupToCreateCommand command,
        CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Удаление группы тестов.
    /// </summary>
    [HttpDelete("/api/testGroup/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteTestGroup(Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new TestGroupToDeleteCommand(id), cancellationToken);

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

        return await mediator.Send(command, cancellationToken);
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
    public async Task<ActionResult<Guid>> CreateHeaderFile(IFormFile file, [FromQuery] Guid? groupId,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new HeaderFileToCreateCommand
        {
            Stream = stream,
            Name = file.Name,
        };

        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление header файла.
    /// </summary>
    [HttpDelete("/api/tests/headerFiles/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteHeaderFile(Guid id, CancellationToken cancellationToken)
        => await mediator.Send(new HeaderFileToDeleteCommand(id), cancellationToken);

    #endregion
}