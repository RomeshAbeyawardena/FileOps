using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace FileOps.Core;

public interface IDirectoryOperation
{
    Task<bool> ExistsAsync(string path, CancellationToken cancellationToken);
    Task<IFileInfo> CreateDirectoryAsync(string path, CancellationToken cancellationToken);
}

public class FileSystemDirectoryOperation : IDirectoryOperation
{
    public Task<IFileInfo> CreateDirectoryAsync(string path, CancellationToken cancellationToken)
    {
        Directory.CreateDirectory(path);
        return Task.FromResult<IFileInfo>(new PhysicalDirectoryInfo(new DirectoryInfo(path)));
    }

    public Task<bool> ExistsAsync(string path, CancellationToken cancellationToken)
    {
        return Task.FromResult(Directory.Exists(path));
    }
}