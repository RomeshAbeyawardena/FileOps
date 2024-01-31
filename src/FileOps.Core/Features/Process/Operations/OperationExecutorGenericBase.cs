using FileOps.Core.Features.Parse;
using Shared;
using System.Reflection.Metadata.Ecma335;

namespace FileOps.Core.Operations;

internal abstract class OperationExecutorBase<TOperationConfiguration>(Operation operation, IClockProvider clockProvider)
    : OperationExecutorBase(operation, clockProvider)
    where TOperationConfiguration : IOperationConfiguration
{
    /// <summary>
    /// Resolves paths based on configuration path rules applied by the configuration and the context
    /// </summary>
    /// <param name="configuration">The configuration to validate and build the path from</param>
    /// <param name="rootPath">The root path that makes up the first part of concatenation</param>
    /// <param name="path">The path to concatenate</param>
    /// <param name="applicablePathRules">The path rules applied in this given context</param>
    /// <param name="pathRules">Path rules to check for</param>
    /// <returns>The resolved path</returns>
    /// <exception cref="ArgumentException">Thrown when multiple rules are applied to <paramref name="pathRules"/></exception>
    protected string ResolvePath(TOperationConfiguration configuration, 
        string rootPath, string path, PathRules applicablePathRules, PathRules pathRules)
    {
        if(pathRules == PathRules.UseForBoth)
        {
            throw new ArgumentException("Must only specify a single rule");
        }

        if(pathRules == PathRules.Unspecified)
        {
            throw new ArgumentException("Must specify a rule");
        }

        var isApplicablePath = applicablePathRules.HasFlag(pathRules) || applicablePathRules == PathRules.Unspecified;
        return configuration.PathResolution == PathResolution.Absolute
            ? path
            : string.IsNullOrWhiteSpace(Configuration?.RootPath)
                ? isApplicablePath ? Path.Combine(rootPath, path) : path
                : isApplicablePath ? Path.Combine(Configuration.RootPath, rootPath, path) 
                    : Path.Combine(Configuration.RootPath, path);
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