using MediatR;

namespace FileOps.Core.Features.Process;

internal class Process(IOperationProcessor operationProcessor) : IRequestHandler<ProcessCommand, OperationLedger>
{

    public async Task<OperationLedger> Handle(ProcessCommand request, CancellationToken cancellationToken)
    {
        var operationLedger = new OperationLedger();
        if(request.Configuration == null)
        {
            throw new NullReferenceException();
        }

        var ops = operationProcessor
            .GetOperators(request.Configuration);
        
        foreach(var op in ops)
        {
            await op.ExecuteAll(operationLedger, cancellationToken);
        }

        return operationLedger;
    }
}
