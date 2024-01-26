namespace FileOps.Core;

[Flags]
internal enum FailureAction
{
    AbortOnError,
    LogError,
    SkipFile
}
