namespace FileOps.Core;

public interface IValidationOperationConfiguration : IFileOperationConfiguration
{
    bool Exists { get; }
}
