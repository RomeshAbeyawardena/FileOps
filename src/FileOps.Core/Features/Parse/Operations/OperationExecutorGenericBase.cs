namespace FileOps.Core.Features.Parse.Operations;

internal abstract class OperationExecutorBase<TOperationConfiguration>(Operation operation)
    : OperationExecutorBase(operation)
    where TOperationConfiguration : IOperationConfiguration
{
    public override bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration is TOperationConfiguration operationConfiguration
            && base.CanExecute(configuration) && this.CanExecute(operationConfiguration);
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