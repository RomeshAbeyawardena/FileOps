using MediatR;

namespace FileOps.Core.Features.Process;

internal record ProcessCommand : IRequest<OperationLedger>
{
    public IFileOpsConfiguration? Configuration { get; set; }
}
