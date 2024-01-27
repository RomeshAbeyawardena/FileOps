namespace FileOps.Core.Features.Parse.Operations;

internal abstract class OperationExecutorBase(Operation operation, OperationLedger ledgerEntries) : IOperationExecutor
{
    protected Task<bool> HandleException(IOperationConfiguration configuration, 
        Exception exception, bool succeeded = false)
    {
        LedgerEntries.Add(new OperationLedgerEntry
        {
            Configuration = configuration,
            Exception = exception,
            Succeeded = succeeded
        });

        if (configuration.FailureAction == FailureAction.AbortOnError)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }

    protected OperationLedger LedgerEntries => ledgerEntries;
    public Operation Operation { get; } = operation;
    
    public virtual bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration.Enabled 
            && configuration.Operation == Operation;
    }

    public abstract Task Execute(IOperationConfiguration configuration, 
        CancellationToken cancellationToken);
}