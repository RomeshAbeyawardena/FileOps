using FileOps.Core;
using FileOps.Core.Operations;
using Microsoft.Extensions.FileProviders;
using Moq;
using Shared;

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
        var clockProviderMock = new Mock<IClockProvider>();
        var ledger = new OperationLedger();
        var executor = new CopyOperationExecutor(fileProviderMock.Object, directoryOperationMock.Object, fileOperationMock.Object, clockProviderMock.Object)
        {
            LedgerEntries = ledger
        };
        await executor.Execute(new CopyOperationConfiguration
        {
            FailureAction = FailureAction.SkipFile
        }, CancellationToken.None);
    }
}
