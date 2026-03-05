namespace TestingMonitor.Application.UseCases.Models;

/// <summary>
/// Compiler.
/// </summary>
public sealed class CompilerDto
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

    /// <summary>
    /// Does local docker image exist?
    /// </summary>
    public bool HasDockerLocally { get; set; }
}
