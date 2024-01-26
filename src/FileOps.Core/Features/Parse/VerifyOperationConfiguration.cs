namespace FileOps.Core;

internal record VerifyOperationConfiguration : OperationConfigurationBase, IValidationOperationConfiguration
{
    public bool Exists { get; }
    public string? RootPath { get; }
    public IEnumerable<string>? Files { get; }
}
