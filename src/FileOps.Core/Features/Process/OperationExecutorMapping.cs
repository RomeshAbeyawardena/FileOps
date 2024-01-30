using FileOps.Core.Operations;

namespace FileOps.Core.Features.Process;

internal record OperationExecutorMapping
{
    public OperationExecutorMapping(IFileOpsConfiguration configuration)
    {
        Configuration = configuration;
        OperationConfiguration = Array.Empty<IOperationConfiguration>();
    }
    public IFileOpsConfiguration Configuration { get; }
    public IOperationExecutor? OperationExecutor { get; init; }
    public IEnumerable<IOperationConfiguration> OperationConfiguration { get; init; }
    public async Task ExecuteAll(OperationLedger operationLedger, 
        CancellationToken cancellationToken)
    {
        foreach(var operationConfiguration in OperationConfiguration)
        {
            if (OperationExecutor != null)
            {
                OperationExecutor.Configuration = Configuration;
                OperationExecutor.LedgerEntries = operationLedger;
                await OperationExecutor.Execute(operationConfiguration, cancellationToken);
            }
        }
    }
}
