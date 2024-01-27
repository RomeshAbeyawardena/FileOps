namespace FileOps.Core.Features.Parse.Operations;

internal interface IOperationExecutor
{
    Operation Operation { get; }
    bool CanExecute(IOperationConfiguration configuration);
    Task Execute(IOperationConfiguration configuration, CancellationToken cancellationToken);
}