using Microsoft.Extensions.FileProviders;

namespace FileOps.Core;

public interface IFileOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CopyFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
    Task<IFileInfo> MoveFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
}
