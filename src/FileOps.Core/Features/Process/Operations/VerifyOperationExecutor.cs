using Scrutor;
using System.Linq;

namespace FileOps.Core.Operations;

[ServiceDescriptor]
internal class VerifyOperationExecutor(IDirectoryOperation directoryOperation,
    IFileOperation fileOperation) : OperationExecutorBase<VerifyOperationConfiguration>(Operation.Verify)
{
    public override async Task Execute(VerifyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        try
        {
            if (configuration.PathResolution == PathResolution.Relative 
                && (string.IsNullOrWhiteSpace(configuration.RootPath) 
                || !await directoryOperation.ExistsAsync(configuration.RootPath, cancellationToken)))
            {
                throw new DirectoryNotFoundException($"Root path not specified or found: {configuration.RootPath} when path resolution is relative");
            }

            if (configuration.Files == null)
            {
                throw new NullReferenceException("No files to process");
            }


            var files = configuration.PathResolution == PathResolution.Absolute
                ? configuration.Files!.ToAsyncEnumerable().WhereAwaitWithCancellation(fileOperation.ExistsAsync)
                : configuration.Files!.ToAsyncEnumerable().WhereAwaitWithCancellation((f,c) => fileOperation.ExistsAsync(
                    Path.Combine(configuration.RootPath!, f), c));

            if (await files.AnyAsync(cancellationToken))
            {
                throw new FileNotFoundException($"The following files could not be found: ${string.Join(',', files)}");
            }
            var exists = true;
            
            LedgerEntries?.Add(new OperationLedgerEntry
            {
                Configuration = configuration,
                Result = exists == configuration.Exists,
                Succeeded = exists == configuration.Exists
            });

        }
        catch (FileNotFoundException exception)
        {
            var exists = false;
            if (!await HandleException(configuration, exception, exists == configuration.Exists))
                throw;
        }
        catch(IOException exception)
        {
            if (!await HandleException(configuration, exception))
                throw;
        }
        catch(NullReferenceException exception)
        {
            if (!await HandleException(configuration, exception))
                throw;
        }
    }
}
