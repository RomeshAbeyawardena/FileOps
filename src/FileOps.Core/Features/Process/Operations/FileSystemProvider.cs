using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;

namespace FileOps.Core;

internal class FileSystemProvider : IFileProvider
{
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        return new FileSystemDirectoryContents(new DirectoryInfo(subpath));
    }

    public IFileInfo GetFileInfo(string subpath)
    {
        return new PhysicalFileInfo(new FileInfo(subpath));
    }

    public IChangeToken Watch(string filter)
    {
        throw new NotImplementedException();
    }
}