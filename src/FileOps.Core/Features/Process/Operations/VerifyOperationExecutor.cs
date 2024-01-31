using Scrutor;
using Shared;
using System.Linq;

namespace FileOps.Core.Operations;

[ServiceDescriptor]
internal class VerifyOperationExecutor(IDirectoryOperation directoryOperation,
    IFileOperation fileOperation, IClockProvider clockProvider) : OperationExecutorBase<VerifyOperationConfiguration>(Operation.Verify, clockProvider)
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

            var files = configuration.Files!.ToAsyncEnumerable()
                .WhereAwaitWithCancellation(async(f,c) => !await fileOperation
                    .ExistsAsync(ResolvePath(configuration, configuration.RootPath!, f, configuration.RootPathRules, PathRules.UseForSource), c));

            if (await files.AnyAsync(cancellationToken))
            {
                var filesArray = await files.ToArrayAsync(cancellationToken);
                throw new FileNotFoundException($"The following files could not be found: ${string.Join(',', filesArray)}");
            }
            var exists = true;
            
            LedgerEntries?.Add(new OperationLedgerEntry(ClockProvider)
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
