using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileOps.Core.Features.Parse.Operations;

internal class CopyOperationExecutor(OperationLedger operationLedgerEntries, IFileProvider fileProvider) : FileOperationExecutorBase<CopyOperationConfiguration>(operationLedgerEntries, 
    fileProvider,
    Operation.Copy)
{
    protected override ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if(file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not found");
        }

        if (file.Exists)
        {
            File.Copy(file.PhysicalPath, Path.Combine(destination, file.Name), true);
            return ValueTask.FromResult(true);
        }

        return Task.FromResult(false);
    }
}
