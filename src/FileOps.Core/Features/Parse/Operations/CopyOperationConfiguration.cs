namespace FileOps.Core;

internal record CopyOperationConfiguration : OperationConfigurationBase, IFileTransferOperationConfiguration
{
    public CopyOperationConfiguration() : base(Operation.Copy)
    {

    }

    public string? To { get; set; }
    public string? RootPath { get; set; }
    public IEnumerable<string>? Files { get; set; }
}
