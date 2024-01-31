using FileOps.Core.Features.Parse;

namespace FileOps.Core;

internal record MoveOperationConfiguration : OperationConfigurationBase, IFileTransferOperationConfiguration
{
    public MoveOperationConfiguration() : base(Operation.Move)
    {

    }

    public string? To { get; set; }
    public string? RootPath { get; set; }
    public IEnumerable<string>? Files { get; set; }
    public PathRules RootPathRules { get; }
}
