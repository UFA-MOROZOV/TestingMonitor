using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestingMonitor.Application.UseCases.Compilers.DownloadDocker;
using TestingMonitor.Application.UseCases.Compilers.ExecuteCode;
using TestingMonitor.Application.UseCases.Compilers.Get;

namespace TestingMonitor.Api.Controllers;

[Route("/api/compilers")]
public sealed class TestsController(IMediator mediator) : Controller
{
    
}