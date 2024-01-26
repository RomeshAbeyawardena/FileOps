namespace FileOps.Core.Features.Parse;

internal record ParseCommand
{
    public string? FileName { get; set; }
    public string? Json { get; set; }
}
