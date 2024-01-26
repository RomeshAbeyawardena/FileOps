using System.Text.Json;
using System.Text.Json.Serialization;
using FileOps.Core.Features.Parse.Operations;

namespace FileOps.Core;

internal record JsonFileOpsConfiguration
{
    public static JsonSerializerOptions GetDefault(JsonSerializerOptions? options = null)
    {
        var opts = options == null 
            ? new JsonSerializerOptions() 
            : new JsonSerializerOptions(options)
        {
            PropertyNameCaseInsensitive = true
        };

        opts.Converters.Add(new JsonStringEnumConverter());
        return opts;
    }

    public static FileOpsConfiguration? Parse(JsonDocument json, 
        JsonSerializerOptions? options = null)
    {
        options = GetDefault(options);

        return json.Deserialize<JsonFileOpsConfiguration>(options);
    }

    public string? RootPath { get; set; }
    public IEnumerable<MoveOperationConfiguration>? Move { get; set; }
    public IEnumerable<CopyOperationConfiguration>? Copy { get; set; }
    public IEnumerable<VerifyOperationConfiguration>? Verify { get; set; }
}
