namespace FileOps.Core.Features.Parse.Operations;

internal interface IOperationExecutor
{
    Operation Operation { get; }
    bool CanExecute(IOperationConfiguration configuration);
    void Execute(IOperationConfiguration configuration);
}

internal abstract class OperationExecutorBase : IOperationExecutor
{
    public OperationExecutorBase(Operation operation)
    {
        Operation = operation;
    }

    public Operation Operation { get; }

    public virtual bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration.Enabled;
    }

    public abstract void Execute(IOperationConfiguration configuration);
}