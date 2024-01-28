using FileOps.Core.Operations;

namespace FileOps.Core.Features.Process;

internal record OperationExecutorMapping
{
    public OperationExecutorMapping()
    {
        OperationConfiguration = Array.Empty<IOperationConfiguration>();
    }

    public IOperationExecutor? OperationExecutor { get; init; }
    public IEnumerable<IOperationConfiguration> OperationConfiguration { get; init; }
    public async Task ExecuteAll(OperationLedger operationLedger, 
        CancellationToken cancellationToken)
    {
        foreach(var operationConfiguration in OperationConfiguration)
        {
            if (OperationExecutor != null)
            {
                OperationExecutor.LedgerEntries = operationLedger;
                await OperationExecutor.Execute(operationConfiguration, cancellationToken);
            }
        }
    }
}


internal interface IOperationProcessor
{
    IEnumerable<OperationExecutorMapping> GetOperators(IFileOpsConfiguration configuration);
}


internal class OperationProcessor(IEnumerable<IOperationExecutor> operationExecutors) : IOperationProcessor
{
    public IEnumerable<OperationExecutorMapping> GetOperators(IFileOpsConfiguration configuration)
    {
        var executorMappingList = new List<OperationExecutorMapping>();
        foreach(var executor in operationExecutors)
        {
            var allConfiguration = Array.Empty<IOperationConfiguration>().AsEnumerable();

            if(configuration.Copy != null)
            {
                allConfiguration = allConfiguration.Union(configuration.Copy);
            }
            if (configuration.Move != null)
            {
                allConfiguration = allConfiguration.Union(configuration.Move);
            }
            if (configuration.Verify != null)
            {
                allConfiguration = allConfiguration.Union(configuration.Verify);
            }
            var applicableConfigurations = allConfiguration.Where(executor.CanExecute);
            if (applicableConfigurations.Any())
            {
                executorMappingList.Add(new OperationExecutorMapping
                {
                    OperationConfiguration = applicableConfigurations,
                    OperationExecutor = executor
                });
            }
        }

        return executorMappingList;
    }
}
