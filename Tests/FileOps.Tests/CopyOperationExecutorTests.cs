using FileOps.Core;
using FileOps.Core.Operations;
using Microsoft.Extensions.FileProviders;
using Moq;

namespace FileOps.Tests;

[TestFixture]
public class CopyOperationExecutorTests
{
    [Test]
    public async Task Execute_does_its_task()
    {
        var fileProviderMock = new Mock<IFileProvider>();
        var directoryOperationMock = new Mock<IDirectoryOperation>();
        var fileOperationMock = new Mock<IFileOperation>();
        var ledger = new OperationLedger();
        var executor = new CopyOperationExecutor(ledger, fileProviderMock.Object, directoryOperationMock.Object, fileOperationMock.Object);

        await executor.Execute(new CopyOperationConfiguration
        {
            FailureAction = FailureAction.SkipFile
        }, CancellationToken.None);
    }
}
