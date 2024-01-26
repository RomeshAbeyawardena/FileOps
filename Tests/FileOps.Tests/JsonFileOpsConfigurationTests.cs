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
        var json = @"{
  ""rootPath"": ""C:\\Source\\Repos"",
  ""move"": [
    {
      ""directoryResolution"": ""UseExistingOrCreateNew"",
      ""description"": ""Moves the generated LIC file compiled by the build agent"",
      ""enabled"": true,
      ""to"": ""output"",
      ""pathResolution"": ""relative"",
      ""rootPath"": ""JsonExtractor.Web"",
      ""failureAction"": ""fail"",
      ""files"": [
        ""cypress.lic""
      ]
    }
  ],
  ""copy"": [
    {
      ""directoryResolution"": ""UseExistingOrCreateNew"",
      ""description"": ""Copy content files to the web root"",
      ""enabled"": true,
      ""to"": ""wwwroot"",
      ""pathResolution"": ""relative"",
      ""rootPath"": ""JsonExtractor.Web"",
      ""failureAction"": ""skipFile"",
      ""files"": [
        ""AuthorInfo.json"",
        ""dark-theme.append.txt""
      ]
    }
  ],
  ""verify"": [
    {
      ""exists"": true,
      ""rootPath"": ""JsonExtractor.Web"",
      ""files"": [
        ""AuthorInfo.json"",
        ""dark-theme.append.txt"",
        ""cypress.lic""
      ]
    }
  ]
}";
        using var jsonDocument = JsonDocument.Parse(json);
        FileOpsConfiguration? configuration = JsonFileOpsConfiguration.Parse(jsonDocument);
        Assert.That(configuration, Is.Not.Null);
    }
}