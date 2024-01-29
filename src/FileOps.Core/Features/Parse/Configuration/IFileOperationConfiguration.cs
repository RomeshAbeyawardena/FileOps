namespace FileOps.Core;

public interface IFileOperationConfiguration : IOperationConfiguration
{
    string? RootPath { get; }
    IEnumerable<string>? Files { get; }
}
