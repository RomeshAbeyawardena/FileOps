namespace FileOps.Core;

internal record OperationLedgerEntry
{
    public IOperationConfiguration? Configuration { get; init; }
    public object? Result { get; init; }
    public bool Succeeded { get; init; }
    public Exception? Exception { get; init; }
}
