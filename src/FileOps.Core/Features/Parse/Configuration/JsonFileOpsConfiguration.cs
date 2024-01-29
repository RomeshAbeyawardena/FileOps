using System.Text.Json;
using System.Text.Json.Serialization;

namespace FileOps.Core;

internal record JsonFileOpsConfiguration : IFileOpsConfiguration
{
    public static JsonSerializerOptions GetDefault(JsonSerializerOptions? options = null)
    {
        var opts = options ?? new JsonSerializerOptions();

        opts.PropertyNameCaseInsensitive = true;
        opts.Converters.Add(new JsonStringEnumConverter());

        return opts;
    }

    public static FileOpsConfiguration? Parse(JsonDocument json, 
        JsonSerializerOptions? options = null)
    {
        options = GetDefault(options);

        var config =  json.Deserialize<JsonFileOpsConfiguration>(options);
        return config;
    }

    public string? RootPath { get; set; }
    public IEnumerable<MoveOperationConfiguration>? Move { get; set; }
    public IEnumerable<CopyOperationConfiguration>? Copy { get; set; }
    public IEnumerable<VerifyOperationConfiguration>? Verify { get; set; }

    IEnumerable<IFileTransferOperationConfiguration>? IFileOpsConfiguration.Copy 
    { 
        get => Copy;
        set => Copy = (IEnumerable<CopyOperationConfiguration>?)value; 
    }
    IEnumerable<IFileTransferOperationConfiguration>? IFileOpsConfiguration.Move
    {
        get => Move;
        set => Move = (IEnumerable<MoveOperationConfiguration>?)value;
    }
    IEnumerable<IValidationOperationConfiguration>? IFileOpsConfiguration.Verify
    {
        get => Verify;
        set => Verify = (IEnumerable<VerifyOperationConfiguration>?)value;
    }
}
