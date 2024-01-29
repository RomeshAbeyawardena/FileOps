using MediatR;

namespace FileOps.Core.Features.Process;

public record ProcessCommand : IRequest<OperationLedger>
{
    public IFileOpsConfiguration? Configuration { get; set; }
}
