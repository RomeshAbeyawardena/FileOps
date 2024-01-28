namespace FileOps.Core;

internal interface IValidationOperationConfiguration : IFileOperationConfiguration
{
    bool Exists { get; }
}
