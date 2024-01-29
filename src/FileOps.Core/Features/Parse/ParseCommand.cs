using MediatR;

namespace FileOps.Core.Features.Parse;

public record ParseCommand : IRequest<IFileOpsConfiguration?>
{
    public string? FileName { get; set; }
    public string? Json { get; set; }
}
