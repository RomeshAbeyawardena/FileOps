using Microsoft.Extensions.FileProviders;

namespace FileOps.Core.Operations;

internal class MoveOperationExecutor(OperationLedger operationLedgerEntry, IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation) : FileOperationExecutorBase<MoveOperationConfiguration>(operationLedgerEntry, fileProvider,
    directoryOperation, Operation.Move)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if (file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not specified");
        }

        if (file.Exists)
        {
            var movedFileInfo = await fileOperation
                .MoveFileAsync(file, Path.Combine(destination, file.Name), 
                cancellationToken, true);

            LedgerEntries.Add(new OperationLedgerEntry
            {
                Configuration = operationConfiguration,
                Result = movedFileInfo,
                Succeeded = movedFileInfo.Exists
            });
            return true;
        }

        LedgerEntries.Add(new OperationLedgerEntry
        {
            Configuration = operationConfiguration,
            Exception = new NullReferenceException("File not found")
        });

        return false;
    }
}
