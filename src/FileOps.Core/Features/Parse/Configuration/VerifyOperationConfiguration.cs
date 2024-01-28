namespace FileOps.Core;

internal record VerifyOperationConfiguration : OperationConfigurationBase, IValidationOperationConfiguration
{
    public VerifyOperationConfiguration()
        : base(Operation.Verify)
    {

    }
    public bool Exists { get; set; }
    public string? RootPath { get; set; }
    public IEnumerable<string>? Files { get; set; }
}