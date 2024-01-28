using Microsoft.Extensions.FileProviders;

namespace FileOps.Core;

public interface IDirectoryOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CreateDirectoryAsync(string path, CancellationToken cancellationToken);
}
