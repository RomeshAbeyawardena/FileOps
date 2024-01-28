using MediatR;

namespace FileOps.Core.Features.Process;

internal record ProcessCommand : IRequest<OperationLedger>
{
    public IFileOperationConfiguration? Configuration { get; set; }
}
