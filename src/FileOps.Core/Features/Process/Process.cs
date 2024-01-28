using FileOps.Core.Features.Parse;
using MediatR;

namespace FileOps.Core.Features.Process;

internal class Process : IRequestHandler<ProcessCommand, OperationLedger>
{
    public async Task<OperationLedger> Handle(ProcessCommand request, CancellationToken cancellationToken)
    {
        var operationLedger = new OperationLedger();
        if(request.Configuration == null)
        {
            throw new NullReferenceException();
        }
        
        await Task.CompletedTask;

        return operationLedger;
    }
}
