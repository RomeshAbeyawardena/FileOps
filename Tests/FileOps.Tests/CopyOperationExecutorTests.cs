using FileOps.Core.Features.Parse.Operations;
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
        var ledger = new OperationLedger();
        var executor = new CopyOperationExecutor(ledger, fileProviderMock.Object);

        await executor.Execute(new CopyOperationConfiguration
        {
            FailureAction = Core.FailureAction.SkipFile
        }, CancellationToken.None);
    }
}
