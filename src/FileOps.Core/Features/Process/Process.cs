﻿using MediatR;

namespace FileOps.Core.Features.Process;

internal class Process(IOperationExecutorMapper operationProcessor) : IRequestHandler<ProcessCommand, OperationLedger>
{

    public async Task<OperationLedger> Handle(ProcessCommand request, CancellationToken cancellationToken)
    {
        var operationLedger = new OperationLedger();
        if(request.Configuration == null)
        {
            throw new NullReferenceException("Configuration not specified");
        }

        var operators = operationProcessor
            .GetMappings(request.Configuration);
        
        foreach(var @operator in operators)
        {
            await @operator.ExecuteAll(operationLedger, cancellationToken);
        }

        return operationLedger;
    }
}
