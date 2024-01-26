﻿namespace FileOps.Core;

internal enum  Operation
{
    Copy,
    Move,
    Verify
}

internal interface IOperationConfiguration
{
    Operation Operation { get; }
    DirectoryResolution DirectoryResolution { get; }
    string? Description { get; }
    bool Enabled { get; }
    PathResolution PathResolution { get; }
    FailureAction FailureAction { get; }
}
