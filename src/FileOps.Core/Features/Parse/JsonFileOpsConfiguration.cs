using System.Text.Json;

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
        }

        return json.Deserialize<JsonFileOpsConfiguration>(options);
    }

    public string? RootPath { get; set; }
    public IEnumerable<MoveOperationConfiguration>? Move { get; set; }
    public IEnumerable<CopyOperationConfiguration>? Copy { get; set; }
    public IEnumerable<VerifyOperationConfiguration>? Verify { get; set; }
}
