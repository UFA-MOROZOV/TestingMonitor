namespace TestingMonitor.Api.Models;

public class CompilerModel
{
    /// <summary>
    /// Имя образа.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Версия.
    /// </summary>
    public string Version { get; set; } = null!;

    /// <summary>
    /// Имя команды.
    /// </summary>
    public string CommandName { get; set; } = null!;

    /// <summary>
    /// Файл.
    /// </summary>
    public IFormFile? File { get; set; }
}
