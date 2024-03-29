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
        const string json = @"{
  ""rootPath"": ""C:\\Source\\Repos"",
  ""move"": [
    {
      ""directoryResolution"": ""CreateDirectories"",
      ""description"": ""Moves the generated LIC file compiled by the build agent"",
      ""enabled"": true,
      ""to"": ""output"",
      ""pathResolution"": ""relative"",
      ""rootPath"": ""JsonExtractor.Web"",
      ""failureAction"": ""AbortOnError"",
      ""files"": [
        ""cypress.lic""
      ]
    }
  ],
  ""copy"": [
    {
      ""directoryResolution"": ""CreateDirectories"",
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
        Assert.That(configuration.RootPath, Is.EqualTo("C:\\Source\\Repos"));
    }
}