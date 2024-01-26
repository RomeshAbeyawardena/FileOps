namespace FileOps.Core;

internal record MoveOperationConfiguration : OperationConfigurationBase, IFileTransferOperationConfiguration
{
    public string? To { get; }
    public string? RootPath { get; }
    public IEnumerable<string>? Files { get; }
}
