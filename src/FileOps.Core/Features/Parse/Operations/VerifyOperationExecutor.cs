
namespace FileOps.Core.Features.Parse.Operations;

internal class VerifyOperationExecutor(OperationLedger operationLedgerEntries) : OperationExecutorBase<VerifyOperationConfiguration>(operationLedgerEntries, Operation.Verify)
{
    public override Task Execute(VerifyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        LedgerEntries.Add(new OperationLedgerEntry
        {
            Configuration = configuration,
        });
        throw new NotImplementedException();

    }
}
