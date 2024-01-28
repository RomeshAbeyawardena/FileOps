namespace FileOps.Core;

internal interface IFileOpsConfiguration
{
    string? RootPath { get; set; }
    IEnumerable<IFileTransferOperationConfiguration>? Move { get; set; }
    IEnumerable<IFileTransferOperationConfiguration>? Copy { get; set; }
    IEnumerable<IValidationOperationConfiguration>? Verify { get; set; }
}

internal record FileOpsConfiguration : IFileOpsConfiguration
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
