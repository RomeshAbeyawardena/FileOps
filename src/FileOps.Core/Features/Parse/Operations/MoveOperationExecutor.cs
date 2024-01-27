
namespace FileOps.Core.Features.Parse.Operations;

internal class MoveOperationExecutor : OperationExecutorBase<MoveOperationConfiguration>
{
    public MoveOperationExecutor() : base(Operation.Move)
    {
    }

    public override Task Execute(MoveOperationConfiguration configuration, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
