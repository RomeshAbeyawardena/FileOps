
namespace FileOps.Core.Features.Parse.Operations;

internal class VerifyOperationExecutor : OperationExecutorBase<VerifyOperationConfiguration>
{
    public VerifyOperationExecutor() : base(Operation.Verify)
    {
    }

    public override Task Execute(VerifyOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
