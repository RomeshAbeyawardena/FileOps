using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileOps.Core;

internal record JsonFileOpsConfiguration
{
    public static FileOpsConfiguration? Parse(JsonDocument json, 
        JsonSerializerOptions? options = null)
    {
        if (options == null)
        {
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
        }

        return json.Deserialize<JsonFileOpsConfiguration>(options);
    }

    public string? RootPath { get; set; }
    public IEnumerable<MoveOperationConfiguration>? Move { get; set; }
    public IEnumerable<CopyOperationConfiguration>? Copy { get; set; }
    public IEnumerable<VerifyOperationConfiguration>? Verify { get; set; }
}
