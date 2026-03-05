using MediatR;

namespace TestingMonitor.Application.UseCases.Compilers.ExecuteTests;

/// <summary>
/// Command of creating a task for compiler to execute.
/// </summary>
public sealed class CompilerToExecuteTestCommand : IRequest<Guid>
{
    /// <summary>
    /// Name for a task.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Compiler Id.
    /// </summary>
    public int CompilerId { get; set; }

    /// <summary>
    ///  Test Id.
    /// </summary>
    public Guid? TestId { get; set; }

    /// <summary>
    /// Test group Id.
    /// </summary>
    public Guid? TestGroupId { get; set; }
}
