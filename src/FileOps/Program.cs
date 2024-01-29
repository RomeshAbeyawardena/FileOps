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
applicationConfiguration.Parse(ArgumentParser.GetParameters(args));

var serviceConfiguration = new ServiceCollection().RegisterServices(new[] {
    typeof(Program).Assembly
});

var services = serviceConfiguration.BuildServiceProvider();
var mediator = services.GetRequiredService<IMediator>();

var fileName = args.FirstOrDefault();
if (!string.IsNullOrWhiteSpace(fileName))
{
    throw new NullReferenceException($"File '{fileName}' not found");
}

args.ElementAtOrDefault(1);



var fileOpsConfiguration = await mediator.Send(new ParseCommand
{
    FileName = fileName
}, cancellationToken);

var processedResult = await mediator
    .Send(new ProcessCommand { Configuration = fileOpsConfiguration },
    cancellationToken);