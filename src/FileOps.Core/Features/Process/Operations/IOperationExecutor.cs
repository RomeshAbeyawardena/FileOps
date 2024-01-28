namespace FileOps.Core.Operations;

internal interface IOperationExecutor
{
    OperationLedger? LedgerEntries { get; set; }
    Operation Operation { get; }
    bool CanExecute(IOperationConfiguration configuration);
    Task Execute(IOperationConfiguration configuration, CancellationToken cancellationToken);
}