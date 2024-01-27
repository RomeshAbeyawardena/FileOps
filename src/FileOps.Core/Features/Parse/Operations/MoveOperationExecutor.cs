
using Microsoft.Extensions.FileProviders;

namespace FileOps.Core.Features.Parse.Operations;

internal class MoveOperationExecutor(OperationLedger operationLedgerEntry, IFileProvider fileProvider) : FileOperationExecutorBase<MoveOperationConfiguration>(operationLedgerEntry, fileProvider, Operation.Move)
{
    protected override ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if (file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not found");
        }

        if (file.Exists)
        {
            File.Move(file.PhysicalPath, Path.Combine(destination, file.Name), true);
            return ValueTask.FromResult(true);
        }

        return ValueTask.FromResult(false);
    }
}
