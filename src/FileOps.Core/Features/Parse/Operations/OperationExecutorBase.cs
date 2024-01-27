namespace FileOps.Core.Features.Parse.Operations;

internal abstract class OperationExecutorBase : IOperationExecutor
{
    public OperationExecutorBase(Operation operation)
    {
        Operation = operation;
    }

    public Operation Operation { get; }

    public virtual bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration.Enabled 
            && configuration.Operation == Operation;
    }

    public abstract Task Execute(IOperationConfiguration configuration, 
        CancellationToken cancellationToken);
}
