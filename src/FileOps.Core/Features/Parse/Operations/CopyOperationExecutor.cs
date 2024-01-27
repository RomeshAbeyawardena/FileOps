﻿using Microsoft.Extensions.FileProviders;

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
                throw new NullReferenceException($"Destination {nameof(configuration.To)} not specified");
            }

            var toPath = configuration.PathResolution == PathResolution.Absolute
                ? configuration.To
                : Path.Combine(configuration.RootPath!, configuration.To);

            if (configuration.DirectoryResolution == DirectoryResolution.CreateDirectories
                && !Directory.Exists(toPath))
            {
                Directory.CreateDirectory(toPath);
            }

            if(configuration.Files == null)
            {
                throw new NullReferenceException("No files to process");
            }

            foreach(var file in configuration.Files)
            {
                var filePath = configuration.PathResolution == PathResolution.Relative
                    ? Path.Combine(configuration.RootPath!, file)
                    : file;

                var fileInfo = fileProvider.GetFileInfo(filePath);
                if (fileInfo.Exists)
                {
                    File.Copy(filePath, Path.Combine(toPath, fileInfo.Name), true);
                }
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
}
