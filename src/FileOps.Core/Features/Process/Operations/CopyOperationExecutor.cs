using Microsoft.Extensions.FileProviders;
using Scrutor;
using Shared;

namespace FileOps.Core.Operations;

[ServiceDescriptor]
internal class CopyOperationExecutor(IFileProvider fileProvider, IDirectoryOperation directoryOperation, IFileOperation fileOperation,
    IClockProvider clockProvider) : FileOperationExecutorBase<CopyOperationConfiguration>( 
    fileProvider, directoryOperation,
    Operation.Copy, clockProvider)
{
    protected override async ValueTask<bool> ProcessFile(IFileTransferOperationConfiguration operationConfiguration, 
        string destination, IFileInfo file, CancellationToken cancellationToken)
    {
        if(file.PhysicalPath == null)
        {
            throw new NullReferenceException("File not specified");
        }

        if (file.Exists)
        {
            var copiedFileInfo = await fileOperation.CopyFileAsync(file, Path.Combine(destination, file.Name), cancellationToken, true);

            LedgerEntries?.Add(new OperationLedgerEntry(ClockProvider)
            {
                Configuration = operationConfiguration,
                Result = copiedFileInfo,
                Succeeded = copiedFileInfo.Exists
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
