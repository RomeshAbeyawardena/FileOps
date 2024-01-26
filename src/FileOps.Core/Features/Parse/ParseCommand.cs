using MediatR;

namespace FileOps.Core.Features.Parse;

internal record ParseCommand : IRequest<FileOpsConfiguration>
{
    public string? FileName { get; set; }
    public string? Json { get; set; }
}
