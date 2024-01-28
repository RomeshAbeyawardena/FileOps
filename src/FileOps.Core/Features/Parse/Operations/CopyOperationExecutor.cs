using Microsoft.Extensions.FileProviders;
using System.IO;

namespace FileOps.Core.Features.Parse.Operations;

internal class CopyOperationExecutor(OperationLedger operationLedgerEntries, IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation) : FileOperationExecutorBase<CopyOperationConfiguration>(operationLedgerEntries, 
    fileProvider, directoryOperation,
    Operation.Copy)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if(file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not found");
        }

        if (file.Exists)
        {
            await fileOperation.CopyFileAsync(file, Path.Combine(destination, file.Name), cancellationToken, true);
            return true;
        }

        return false;
    }
}
