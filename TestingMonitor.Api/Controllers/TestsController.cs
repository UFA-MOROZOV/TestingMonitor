using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.Get;
using TestingMonitor.Application.UseCases.Tests.Create;
using TestingMonitor.Application.UseCases.Tests.Delete;
using TestingMonitor.Application.UseCases.Tests.Get;
using TestingMonitor.Application.UseCases.Tests.Groups.Create;
using TestingMonitor.Application.UseCases.Tests.Groups.Delete;
using TestingMonitor.Application.UseCases.Tests.Groups.Upload;

namespace TestingMonitor.Api.Controllers;

[Route("/api/tests")]
public sealed class TestsController(IMediator mediator) : Controller
{
    /// <summary>
    /// Создание теста.
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> CreateTest([FromForm] IFormFile file, [FromQuery] Guid? groupId,
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
    [HttpPost("/api/tests/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UploadTests([FromForm] IFormFile file, [FromQuery] Guid? groupId,
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
}