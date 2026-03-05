using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Delete;

/// <summary>
/// Comand of a compiler deletion.
/// </summary>
public sealed class CompilerToDeleteCommand(int id) : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; } = id;
}
