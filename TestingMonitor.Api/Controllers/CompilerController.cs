using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.Get;

namespace TestingMonitor.Api.Controllers;

[Route("/api/controllers")]
public sealed class CompilerController(IMediator mediator) : Controller
{
    [HttpGet("/api/controllers")]
    [ProducesResponseType<List<CompilerDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CompilerDto>>> GetCompilers(CancellationToken cancellationToken)
        => await mediator.Send(new GetCompilersQuery(), cancellationToken);
}
