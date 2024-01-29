using FileOps;
using FileOps.Core;
using FileOps.Core.Features.Parse;
using FileOps.Core.Features.Process;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Console;
using Shared.Console.Extensions;

using var cancellationTokenRegistration = new CancellationTokenRegistration();
var cancellationToken = cancellationTokenRegistration.Token;

ApplicationConfiguration applicationConfiguration = new();
applicationConfiguration.Parse(ArgumentParser.GetParameters(args, false));

var serviceConfiguration = new ServiceCollection().RegisterServices(new[] {
    typeof(Program).Assembly
});

var services = serviceConfiguration.BuildServiceProvider();
var mediator = services.GetRequiredService<IMediator>();

if (string.IsNullOrWhiteSpace(applicationConfiguration.FileName))
{
    throw new NullReferenceException($"File '{applicationConfiguration.FileName}' not found");
}

var fileOpsConfiguration = await mediator.Send(new ParseCommand
{
    FileName = applicationConfiguration.FileName,
    Json = applicationConfiguration.Json
}, cancellationToken);

var processedResult = await mediator
    .Send(new ProcessCommand { Configuration = fileOpsConfiguration },
    cancellationToken);