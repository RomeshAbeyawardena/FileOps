
using System;

namespace FileOps.Core.Features.Parse.Operations;

internal class VerifyOperationExecutor(OperationLedger operationLedgerEntries) : OperationExecutorBase<VerifyOperationConfiguration>(operationLedgerEntries, Operation.Verify)
{
    public override async Task Execute(VerifyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        Task HandleException(Exception exception, bool succeeded = false)
        {
            LedgerEntries.Add(new OperationLedgerEntry
            {
                Configuration = configuration,
                Exception = exception,
                Succeeded = succeeded
            });
            return Task.CompletedTask;
        }

        try
        {
            if (!Directory.Exists(configuration.RootPath))
            {
                throw new DirectoryNotFoundException($"Root path not found: {configuration.RootPath}");
            }

            if (configuration.Files == null)
            {
                throw new NullReferenceException("No files to process");
            }

            if(configuration.PathResolution == PathResolution.Absolute
                ? configuration.Files.Any(f => !File.Exists(f))
                : configuration.Files!.Any(f => !File.Exists(
                    Path.Combine(configuration.RootPath, f)))
                )
            {
                throw new FileNotFoundException();
            }
            var exists = true;
            
            LedgerEntries.Add(new OperationLedgerEntry
            {
                Configuration = configuration,
                Result = true,
                Succeeded = exists == configuration.Exists
            });

        }
        catch (FileNotFoundException exception)
        {
            var exists = false;
            await HandleException(exception, exists == configuration.Exists);
        }
        catch(IOException exception)
        {
            await HandleException(exception);
        }
        catch(NullReferenceException exception)
        {
            await HandleException(exception);
        }
    }
}
