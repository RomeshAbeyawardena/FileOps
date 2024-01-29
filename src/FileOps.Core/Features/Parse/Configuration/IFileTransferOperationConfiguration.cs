namespace FileOps.Core;

public interface IFileTransferOperationConfiguration : IFileOperationConfiguration
{
    string? To { get; }
}
