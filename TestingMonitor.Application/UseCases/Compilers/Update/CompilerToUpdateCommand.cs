using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.Update;

/// <summary>
/// Command of updating a compiler.
/// </summary>
public sealed class CompilerToUpdateCommand : IRequest<Unit>
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Version.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Command to execute.
    /// </summary>
    public string CommandName { get; set; } = null!;
}
