namespace FileOps.Core;

public enum  Operation
{
    Copy,
    Move,
    Verify
}

public interface IOperationConfiguration
{
    Operation Operation { get; }
    DirectoryResolution DirectoryResolution { get; }
    string? Description { get; }
    bool Enabled { get; }
    PathResolution PathResolution { get; }
    FailureAction FailureAction { get; }
}
