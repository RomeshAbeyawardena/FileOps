using Microsoft.Extensions.FileProviders;

namespace FileOps.Core.Features.Parse.Operations;

internal class CopyOperationExecutor(OperationLedger operationLedgerEntries, IFileProvider fileProvider) : OperationExecutorBase<CopyOperationConfiguration>(operationLedgerEntries, Operation.Copy)
{
    public override async Task Execute(CopyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        try
        {
            if (configuration.PathResolution == PathResolution.Relative
                && string.IsNullOrWhiteSpace(configuration.RootPath))
            {
                throw new NullReferenceException("Root path must be specified");
            }

            if (string.IsNullOrWhiteSpace(configuration.To))
            {
                throw new NullReferenceException("Destination (To) not specified");
            }

            if (configuration.DirectoryResolution == DirectoryResolution.CreateDirectories
                && !Directory.Exists(configuration.To))
            {
                Directory.CreateDirectory(configuration.To);
            }
        }
        catch (NullReferenceException exception)
        {
            if (!await HandleException(configuration, exception))
            {
                throw;
            }
        }
    }
}
