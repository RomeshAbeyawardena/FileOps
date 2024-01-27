namespace FileOps.Core.Features.Parse.Operations;

internal class CopyOperationExecutor(OperationLedger operationLedgerEntries) : OperationExecutorBase<CopyOperationConfiguration>(operationLedgerEntries, Operation.Copy)
{
    public override bool CanExecute(CopyOperationConfiguration configuration)
    {
        return base.CanExecute(configuration);
    }

    public override Task Execute(CopyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
