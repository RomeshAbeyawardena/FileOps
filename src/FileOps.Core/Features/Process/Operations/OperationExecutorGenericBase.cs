using Shared;
using System.Reflection.Metadata.Ecma335;

namespace FileOps.Core.Operations;

internal abstract class OperationExecutorBase<TOperationConfiguration>(Operation operation, IClockProvider clockProvider)
    : OperationExecutorBase(operation, clockProvider)
    where TOperationConfiguration : IOperationConfiguration
{
    protected string ResolvePath(TOperationConfiguration configuration, 
        string rootPath, string path)
    {
        return configuration.PathResolution == PathResolution.Absolute
            ? path
            : string.IsNullOrWhiteSpace(Configuration?.RootPath)
                ? Path.Combine(rootPath, path)
                : Path.Combine(Configuration.RootPath, rootPath, path);
    }

    public override bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration is TOperationConfiguration operationConfiguration
            && base.CanExecute(configuration) 
            && this.CanExecute(operationConfiguration);
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