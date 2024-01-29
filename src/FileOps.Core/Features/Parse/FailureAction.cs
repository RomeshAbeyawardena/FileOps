namespace FileOps.Core;

[Flags]
public enum FailureAction
{
    AbortOnError,
    LogError,
    SkipFile
}
