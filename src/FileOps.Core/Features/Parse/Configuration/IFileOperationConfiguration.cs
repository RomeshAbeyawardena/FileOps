using FileOps.Core.Features.Parse;

namespace FileOps.Core;

public interface IFileOperationConfiguration : IOperationConfiguration
{
    PathRules RootPathRules { get; }
    string? RootPath { get; }
    IEnumerable<string>? Files { get; }
}
