using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.HeaderFiles.Create;
using TestingMonitor.Application.UseCases.HeaderFiles.Delete;
using TestingMonitor.Application.UseCases.HeaderFiles.Get;
using TestingMonitor.Application.UseCases.HeaderFiles.GetContent;
using TestingMonitor.Application.UseCases.HeaderFiles.UpdateContent;
using TestingMonitor.Application.UseCases.HeaderFiles.Upload;
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
using TestingMonitor.Application.UseCases.Tests.Upload;

namespace TestingMonitor.Api.Controllers;

[Route("/api/tests")]
[Authorize]
public sealed class TestsController(IMediator mediator) : Controller
{
    /// <summary>
    /// Get all tests.
    /// </summary>
    [HttpGet("/api/tests")]
    [ProducesResponseType<GetTestsResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTestsResponse>> GetTests(GetTestsQuery query,
        CancellationToken cancellationToken)
        => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Upload a test.
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
    /// Create a test.
    /// </summary>
    [HttpPost("/api/tests")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateTest([FromBody] TestToCreateCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Get tests content.
    /// </summary>
    [HttpGet("/api/tests/{id:guid}/content")]
    [ProducesResponseType<GetTestContentByIdResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetTestContentByIdResponse>> GetContent(Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetTestContentByIdQuery(id), cancellationToken);

    /// <summary>
    /// Update a test.
    /// </summary>
    [HttpPut("/api/tests")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateTest([FromBody] TestToUpdateCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Delete a test.
    /// </summary>
    [HttpDelete("/api/tests/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteTest(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new TestToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Update test header files.
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
    /// Create a test group.
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
    /// Delete a test group.
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
    /// Upload a test group using zip.
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
    /// Update test groups header files.
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
    /// Get all header files.
    /// </summary>
    [HttpGet("/api/headerFiles")]
    [ProducesResponseType<GetHeaderFilesResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetHeaderFilesResponse>> CreateHeaderFile(GetHeaderFilesQuery query,
        CancellationToken cancellationToken)
        => await mediator.Send(query, cancellationToken);

    /// <summary>
    /// Upload a header file.
    /// </summary>
    [HttpPost("/api/headerFiles/upload")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateHeaderFile(IFormFile file,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();

        var command = new HeaderFileToUploadCommand
        {
            Stream = stream,
            Name = file.FileName,
        };

        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Create a header file.
    /// </summary>
    [HttpPost("/api/headerFiles")]
    [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> CreateHeaderFile([FromBody] HeaderFileToCreateCommand command, CancellationToken cancellationToken)
        => await mediator.Send(command, cancellationToken);

    /// <summary>
    /// Delete a header file.
    /// </summary>
    [HttpDelete("/api/headerFiles/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> DeleteHeaderFile(Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new HeaderFileToDeleteCommand(id), cancellationToken);

        return NoContent();
    }

    /// <summary>
    /// Get header files content.
    /// </summary>
    [HttpGet("/api/headerFiles/{id:guid}/content")]
    [ProducesResponseType<GetHeaderFileContentByIdResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<GetHeaderFileContentByIdResponse>> GetHeaderFileContent(Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetHeaderFileContentByIdQuery(id), cancellationToken);

    /// <summary>
    /// Update a header file.
    /// </summary>
    [HttpPut("/api/headerFiles")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult<Unit>> UpdateHeader([FromBody] HeaderFileToUpdateCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);

        return NoContent();
    }

    #endregion
}