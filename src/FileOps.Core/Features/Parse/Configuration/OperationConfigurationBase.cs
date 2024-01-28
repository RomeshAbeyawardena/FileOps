namespace FileOps.Core;

internal abstract record OperationConfigurationBase : IOperationConfiguration
{
    public OperationConfigurationBase(Operation operation)
    {
        Operation = operation;
    }

    public DirectoryResolution DirectoryResolution { get; set; }
    public string? Description { get; set; }
    public bool Enabled { get; set; }
    public PathResolution PathResolution { get; set; }
    public FailureAction FailureAction { get; set; }
    public Operation Operation { get; set; }
}