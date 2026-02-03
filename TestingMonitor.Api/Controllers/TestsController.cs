using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilers")]
public sealed class TestsController(IMediator mediator) : Controller
{

}