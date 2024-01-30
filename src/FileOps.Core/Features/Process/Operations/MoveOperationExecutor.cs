using Microsoft.Extensions.FileProviders;
using Scrutor;
using Shared;

namespace FileOps.Core.Operations;

[ServiceDescriptor]
internal class MoveOperationExecutor(IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation,
    IClockProvider clockProvider) : FileOperationExecutorBase<MoveOperationConfiguration>(fileProvider,
    directoryOperation, Operation.Move, clockProvider)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, string destination, 
        IFileInfo file, CancellationToken cancellationToken)
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

            LedgerEntries?.Add(new OperationLedgerEntry(ClockProvider)
            {
                Configuration = operationConfiguration,
                Result = movedFileInfo,
                Succeeded = movedFileInfo.Exists
            });
            return true;
        }

        LedgerEntries?.Add(new OperationLedgerEntry(ClockProvider)
        {
            Configuration = operationConfiguration,
            Exception = new NullReferenceException("File not found")
        });

        return false;
    }
}
