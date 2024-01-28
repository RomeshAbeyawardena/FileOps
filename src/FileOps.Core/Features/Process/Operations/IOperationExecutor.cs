namespace FileOps.Core.Operations;

internal interface IOperationExecutor
{
    Operation Operation { get; }
    bool CanExecute(IOperationConfiguration configuration);
    Task Execute(IOperationConfiguration configuration, CancellationToken cancellationToken);
}