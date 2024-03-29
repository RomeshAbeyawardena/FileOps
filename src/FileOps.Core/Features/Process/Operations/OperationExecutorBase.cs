﻿using Shared;

namespace FileOps.Core.Operations;

internal abstract class OperationExecutorBase(Operation operation,
    IClockProvider clockProvider) : IOperationExecutor
{
    protected IClockProvider ClockProvider => clockProvider;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="exception"></param>
    /// <param name="succeeded"></param>
    /// <returns>A task that determines where the exception was handled</returns>
    protected ValueTask<bool> HandleException(IOperationConfiguration configuration, 
        Exception exception, bool succeeded = false)
    {
        LedgerEntries?.Add(new OperationLedgerEntry(clockProvider)
        {
            Configuration = configuration,
            Exception = exception,
            Succeeded = succeeded
        });

        if (configuration.FailureAction == FailureAction.AbortOnError)
        {
            return ValueTask.FromResult(false);
        }

        return ValueTask.FromResult(true);
    }

    public IFileOpsConfiguration? Configuration { get; set; }
    public OperationLedger? LedgerEntries { get; set; }
    public Operation Operation { get; } = operation;
    
    public virtual bool CanExecute(IOperationConfiguration configuration)
    {
        return configuration.Enabled 
            && configuration.Operation == Operation;
    }

    public abstract Task Execute(IOperationConfiguration configuration, 
        CancellationToken cancellationToken);
}