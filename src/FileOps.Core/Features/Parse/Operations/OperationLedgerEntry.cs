namespace FileOps.Core.Features.Parse.Operations;

internal record OperationLedgerEntry
{
    public IOperationConfiguration? Configuration { get; init; }
    public bool Result { get; init; }
    public bool Succeeded { get; init; }
    public Exception? Exception { get; init; }
}
