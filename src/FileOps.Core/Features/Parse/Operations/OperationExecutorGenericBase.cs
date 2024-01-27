namespace FileOps.Core.Features.Parse.Operations;

internal abstract class OperationExecutorBase<TOperationConfiguration>
    : OperationExecutorBase
    where TOperationConfiguration : IOperationConfiguration
{
    public OperationExecutorBase(Operation operation) : base(operation) { }

    public override bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration is TOperationConfiguration operationConfiguration
            && base.CanExecute(configuration) && this.CanExecute(configuration);
    }

    public override Task Execute(IOperationConfiguration configuration, 
        CancellationToken cancellationToken)
    {
        if (configuration is TOperationConfiguration operationConfiguration)
        {
            return Execute(operationConfiguration, cancellationToken);
        }

        return Task.CompletedTask;
    }

    public virtual bool CanExecute(TOperationConfiguration configuration)
    {
        return true;
    }

    public abstract Task Execute(TOperationConfiguration configuration,
        CancellationToken cancellationToken);
}