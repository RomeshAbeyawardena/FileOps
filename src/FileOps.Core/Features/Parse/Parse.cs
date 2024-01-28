using MediatR;
using Microsoft.Extensions.FileProviders;
using System.Text.Json;

namespace FileOps.Core.Features.Parse;

internal class Parse(IFileProvider fileProvider) : IRequestHandler<ParseCommand, IFileOpsConfiguration?>
{
    public async Task<IFileOpsConfiguration?> Handle(ParseCommand request, CancellationToken cancellationToken)
    {
        var json = request.Json;
        if (string.IsNullOrWhiteSpace(json) && !string.IsNullOrWhiteSpace(request.FileName))
        {
            var file = fileProvider.GetFileInfo(request.FileName);
            if (!file.Exists)
            {
                throw new FileNotFoundException();
            }

            using var streamReader = new StreamReader(file.CreateReadStream());
            json = await streamReader.ReadToEndAsync(cancellationToken);
        }

        if (string.IsNullOrWhiteSpace(json))
        {
            throw new NullReferenceException("Unable to parse JSON from request or requested file");
        }
        using var jsonDocument = JsonDocument.Parse(json);

        return JsonFileOpsConfiguration.Parse(jsonDocument);
    }
}
