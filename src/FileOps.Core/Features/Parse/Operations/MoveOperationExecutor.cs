
using Microsoft.Extensions.FileProviders;

namespace FileOps.Core.Features.Parse.Operations;

internal class MoveOperationExecutor(OperationLedger operationLedgerEntry, IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation) : FileOperationExecutorBase<MoveOperationConfiguration>(operationLedgerEntry, fileProvider,
    directoryOperation, Operation.Move)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if (file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not found");
        }

        if (file.Exists)
        {
            await fileOperation
                .MoveFileAsync(file, Path.Combine(destination, file.Name), 
                cancellationToken, true);
            return true;
        }

        return false;
    }
}
