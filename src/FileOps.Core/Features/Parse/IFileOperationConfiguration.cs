namespace FileOps.Core;

internal interface IFileOperationConfiguration : IOperationConfiguration
{
    string? RootPath { get; }
    IEnumerable<string>? Files { get; }
}
