using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TestingMonitor.Api.Controllers;

[Route("/api/tests")]
public sealed class TestsController(IMediator mediator) : Controller
{

}