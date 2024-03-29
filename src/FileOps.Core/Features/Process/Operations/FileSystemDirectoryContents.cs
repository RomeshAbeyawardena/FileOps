﻿using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
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
