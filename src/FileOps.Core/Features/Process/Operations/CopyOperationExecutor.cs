using Microsoft.Extensions.FileProviders;

namespace FileOps.Core.Operations;

internal class CopyOperationExecutor(IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation) : FileOperationExecutorBase<CopyOperationConfiguration>( 
    fileProvider, directoryOperation,
    Operation.Copy)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if(file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not specified");
        }

        if (file.Exists)
        {
            var copiedFileInfo = await fileOperation.CopyFileAsync(file, Path.Combine(destination, file.Name), cancellationToken, true);

            LedgerEntries?.Add(new OperationLedgerEntry
            {
                Configuration = operationConfiguration,
                Result = copiedFileInfo,
                Succeeded = copiedFileInfo.Exists
            });
            return true;
        }

        LedgerEntries?.Add(new OperationLedgerEntry
        {
            Configuration = operationConfiguration,
            Exception = new NullReferenceException("File not found")
        });

        return false;
    }
}
