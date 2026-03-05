using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.DeleteImage;

/// <summary>
/// Command of a compiler docker image deletion
/// </summary>
public sealed class CompilerToDeleteImageCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; } = id;
}
