namespace FileOps.Core;

internal abstract record OperationConfigurationBase : IOperationConfiguration
{
    public DirectoryResolution DirectoryResolution { get; }
    public string? Description { get; }
    public bool Enabled { get; }
    public PathResolution PathResolution { get; }
    public FailureAction FailureAction { get; }
}