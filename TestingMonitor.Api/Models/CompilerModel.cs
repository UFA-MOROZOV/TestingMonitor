namespace TestingMonitor.Api.Models;

public class CompilerModel
{
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
    /// File.
    /// </summary>
    public IFormFile? File { get; set; }
}
