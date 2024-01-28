using Microsoft.Extensions.FileProviders;

namespace FileOps.Core;

internal interface IDirectoryOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CreateDirectoryAsync(string path, CancellationToken cancellationToken);
}

internal interface IFileOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CopyFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
    Task<IFileInfo> MoveFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
}
