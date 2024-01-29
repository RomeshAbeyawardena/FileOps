using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Primitives;
using System.Collections;

namespace FileOps.Core;

internal record FileSystemDirectoryContents(DirectoryInfo Directory) : IDirectoryContents
{
    private IEnumerable<IFileInfo>? files;
    public bool Exists { get => Directory.Exists; }

    private IEnumerable<IFileInfo> Files
    {
        get
        {
            if(files != null)
            {
                return files;
            }

            var fileList = new List<IFileInfo>();

            fileList.AddRange(Directory
                .EnumerateDirectories()
                .Select(s => (IFileInfo)new PhysicalDirectoryInfo(s)));

            fileList.AddRange(Directory
                .EnumerateFiles()
                .Select(s => (IFileInfo)new PhysicalFileInfo(s)));

            return files = fileList.ToArray();
        }
    }

    public IEnumerator<IFileInfo> GetEnumerator()
    {
        return Files.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Files.GetEnumerator();
    }
}

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