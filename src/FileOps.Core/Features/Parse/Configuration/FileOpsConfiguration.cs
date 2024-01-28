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
