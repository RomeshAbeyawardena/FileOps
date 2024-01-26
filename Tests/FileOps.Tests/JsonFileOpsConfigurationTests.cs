using FileOps.Core;
using System.Text.Json;

namespace FileOps.Tests;

public class JsonFileOpsConfigurationTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void JsonFileOpsConfiguration_Parse()
    {
        var json = "";
        using var jsonDocument = JsonDocument.Parse(json);
        FileOpsConfiguration? configuration = JsonFileOpsConfiguration.Parse(jsonDocument);
        Assert.That(configuration, Is.Not.Null);
    }
}