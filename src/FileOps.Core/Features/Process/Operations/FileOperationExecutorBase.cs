using Microsoft.Extensions.FileProviders;
using Shared;

namespace FileOps.Core.Operations;

internal abstract class FileOperationExecutorBase<TFileOperationConfiguration>(IFileProvider fileProvider, 
    IDirectoryOperation directoryOperation, Operation operation, IClockProvider clockProvider) : OperationExecutorBase<TFileOperationConfiguration>(operation, clockProvider)
    where TFileOperationConfiguration : IFileTransferOperationConfiguration
{
    protected IFileProvider FileProvider => fileProvider;
    public override async Task Execute(TFileOperationConfiguration configuration, CancellationToken cancellationToken)
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
                throw new NullReferenceException($"Destination {nameof(configuration.To)} not specified");
            }

            var toPath = ResolvePath(configuration, configuration.RootPath!, configuration.To, configuration.RootPathRules, PathRules.UseForTarget);

            if (configuration.DirectoryResolution == DirectoryResolution.CreateDirectories
                && !await directoryOperation.ExistsAsync(toPath, cancellationToken))
            {
                await directoryOperation.CreateDirectoryAsync(toPath, cancellationToken);
            }

            if (configuration.Files == null)
            {
                throw new NullReferenceException("No files to process");
            }

            foreach (var file in configuration.Files)
            {
                var filePath = ResolvePath(configuration, configuration.RootPath!, file, configuration.RootPathRules, PathRules.UseForSource);

                var fileInfo = fileProvider.GetFileInfo(filePath);
                await ProcessFile(configuration, toPath, fileInfo, cancellationToken);
            }
        }
        catch (IOException exception)
        {
            if (!await HandleException(configuration, exception))
            {
                throw;
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

    protected abstract ValueTask<bool> ProcessFile(
        IFileTransferOperationConfiguration operationConfiguration, string destination, 
        IFileInfo file, CancellationToken cancellationToken);
}
