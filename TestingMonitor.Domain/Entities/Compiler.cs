namespace TestingMonitor.Domain.Entities;

/// <summary>
/// Compiler.
/// </summary>
public sealed class Compiler
{
    /// <summary>
    /// Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Image name.
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
    /// Full image name.
    /// </summary>
    public string ImageName { get; set;} = null!;

    /// <summary>
    /// Is there a local docker.
    /// </summary>
    public bool HasDockerLocally { get; set; }
}
