using FileOps.Core.Features.Parse.Operations;
using FileOps.Core.FileOps.Core.Features.Parse.FileOps.Core.Features.Parse;
using FileOps.Core.Operations;
using System.Text.Json;

namespace FileOps.Core;

internal record FileOpsConfiguration
{
    public static implicit operator FileOpsConfiguration?(JsonFileOpsConfiguration? configuration)
    {
        if (configuration == null)
        {
            return null;
        }

        return new FileOpsConfiguration
        {
            RootPath = configuration.RootPath,
            Move = configuration.Move,
            Copy = configuration.Copy,
            Verify = configuration.Verify,
        };

    }

    public string? RootPath { get; set; }
    public IEnumerable<IFileTransferOperationConfiguration>? Move { get; set; }
    public IEnumerable<IFileTransferOperationConfiguration>? Copy { get; set; }
    public IEnumerable<IValidationOperationConfiguration>? Verify { get; set; }
}

internal record JsonFileOpsConfiguration
{
    public static FileOpsConfiguration? Parse(JsonDocument json)
    {
        return json.Deserialize<JsonFileOpsConfiguration>();
    }

    public string? RootPath { get; set; }
    public IEnumerable<MoveOperationConfiguration>? Move { get; set; }
    public IEnumerable<CopyOperationConfiguration>? Copy { get; set; }
    public IEnumerable<VerifyOperationConfiguration>? Verify { get; set; }
}

internal record FileOpsConfiguration
{
    public string? RootPath { get; set; }
    public IEnumerable<IFileTransferOperationConfiguration>? Move { get; set; }
    public IEnumerable<IFileTransferOperationConfiguration>? Copy { get; set; }
    public IEnumerable<IValidationOperationConfiguration>? Verify { get; set; }
}

internal enum DirectoryResolution
{
    CreateDirectories,
    UseExisting
}

internal enum PathResolution
{
    Absolute,
    Relative
}

[Flags]
internal enum FailureAction
{
    AbortOnError,
    LogError,
    SkipFile
}

internal interface IOperationConfiguration
{
    DirectoryResolution DirectoryResolution { get; }
    string? Description { get; }
    bool Enabled { get; }
    PathResolution PathResolution { get; }
    FailureAction FailureAction { get; }
}

internal interface IFileOperationConfiguration : IOperationConfiguration
{
    string? RootPath { get; }
    IEnumerable<string>? Files { get; }
}

internal interface IFileTransferOperationConfiguration : IFileOperationConfiguration
{
    string? To { get; }
}

internal interface IValidationOperationConfiguration : IFileOperationConfiguration
{
    bool Exists { get; }
}