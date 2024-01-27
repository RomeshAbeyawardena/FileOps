
namespace FileOps.Core.Features.Parse.Operations;

internal class MoveOperationExecutor(OperationLedger operationLedgerEntry) : OperationExecutorBase<MoveOperationConfiguration>(operationLedgerEntry, Operation.Move)
{
    public override Task Execute(MoveOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
