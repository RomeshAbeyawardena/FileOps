namespace FileOps.Core;

internal interface IFileTransferOperationConfiguration : IFileOperationConfiguration
{
    string? To { get; }
}
