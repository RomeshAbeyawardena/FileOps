using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace FileOps.Core;

public interface IFileOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CopyFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
    Task<IFileInfo> MoveFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false);
}

public class FileSystemOperation : IFileOperation
{
    public Task<IFileInfo> CopyFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileInfo.PhysicalPath);
        if (fileInfo.Exists)
        {
            File.Copy(fileInfo.PhysicalPath, newFilePath, overwrite);
            return Task.FromResult<IFileInfo>(
            new PhysicalFileInfo(new FileInfo(newFilePath)));
        }

        return Task.FromResult<IFileInfo>(
            new PhysicalFileInfo(new FileInfo(newFilePath)));
    }

    public Task<bool> ExistsAsync(string path, CancellationToken cancellationToken)
    {
        return Task.FromResult(File.Exists(path));
    }

    public Task<IFileInfo> MoveFileAsync(IFileInfo fileInfo, string newFilePath, CancellationToken cancellationToken, bool overwrite = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileInfo.PhysicalPath);
        if (fileInfo.Exists)
        {
            File.Move(fileInfo.PhysicalPath, newFilePath, overwrite);
            return Task.FromResult<IFileInfo>(
            new PhysicalFileInfo(new FileInfo(newFilePath)));
        }

        return Task.FromResult<IFileInfo>(
            new PhysicalFileInfo(new FileInfo(newFilePath)));
    }
}