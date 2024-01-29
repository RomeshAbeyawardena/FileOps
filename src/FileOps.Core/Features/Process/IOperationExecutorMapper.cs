using FileOps.Core.Operations;

namespace FileOps.Core.Features.Process;

internal interface IOperationExecutorMapper
{
    IEnumerable<OperationExecutorMapping> GetMappings(IFileOpsConfiguration configuration);
}


internal class OperationExecutorMapper(IEnumerable<IOperationExecutor> operationExecutors) : IOperationExecutorMapper
{
    public IEnumerable<OperationExecutorMapping> GetMappings(IFileOpsConfiguration configuration)
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
