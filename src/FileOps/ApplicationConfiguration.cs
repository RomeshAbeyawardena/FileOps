using Shared;

namespace FileOps;

internal class ApplicationConfiguration
{
    [ArgumentDescriptor(ArgumentName = "file-name")]
    public string? FileName { get; set; }
    [ArgumentDescriptor(ArgumentName = "json-value")]
    public string? Json { get; set; }
}
