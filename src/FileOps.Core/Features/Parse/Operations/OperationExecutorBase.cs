namespace FileOps.Core.Features.Parse.Operations;

internal abstract class OperationExecutorBase(Operation operation) : IOperationExecutor
{
    public Operation Operation { get; } = operation;

    public virtual bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration.Enabled 
            && configuration.Operation == Operation;
    }

    public abstract Task Execute(IOperationConfiguration configuration, 
        CancellationToken cancellationToken);
}
