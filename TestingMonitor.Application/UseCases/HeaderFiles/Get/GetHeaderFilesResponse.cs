namespace TestingMonitor.Application.UseCases.HeaderFiles.Get;

/// <summary>
/// Result of getting header files
/// </summary>
public sealed class GetHeaderFilesResponse
{
    /// <summary>
    /// Header files
    /// </summary>
    public ICollection<HeaderFileDto> HeaderFiles { get; set; } = [];
}

/// <summary>
/// Header file.
/// </summary>
public sealed class HeaderFileDto
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = null!;
}