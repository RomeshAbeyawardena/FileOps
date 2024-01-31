using FileOps.Core.Features.Parse;

namespace FileOps.Core;

internal record CopyOperationConfiguration : OperationConfigurationBase, IFileTransferOperationConfiguration
{
    public CopyOperationConfiguration() : base(Operation.Copy)
    {

    }

    public string? To { get; set; }
    public string? RootPath { get; set; }
    public IEnumerable<string>? Files { get; set; }
    public PathRules RootPathRules { get; }
}
