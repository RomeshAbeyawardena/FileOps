using Scrutor;

namespace FileOps.Core.Operations;

[ServiceDescriptor]
internal class VerifyOperationExecutor() : OperationExecutorBase<VerifyOperationConfiguration>(Operation.Verify)
{
    public override async Task Execute(VerifyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        try
        {
            if (configuration.PathResolution == PathResolution.Relative 
                && string.IsNullOrWhiteSpace(configuration.RootPath) 
                || !Directory.Exists(configuration.RootPath))
            {
                throw new DirectoryNotFoundException($"Root path not found: {configuration.RootPath} when path resolution is relative");
            }

            if (configuration.Files == null)
            {
                throw new NullReferenceException("No files to process");
            }

            var files = configuration.PathResolution == PathResolution.Absolute
                ? configuration.Files.Where(f => !File.Exists(f))
                : configuration.Files!.Where(f => !File.Exists(
                    Path.Combine(configuration.RootPath, f)));

            if (files.Any())
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
