namespace FileOps.Core;

internal record CopyOperationConfiguration : OperationConfigurationBase, IFileTransferOperationConfiguration
{
    public string? To { get; }
    public string? RootPath { get; }
    public IEnumerable<string>? Files { get; }
}
