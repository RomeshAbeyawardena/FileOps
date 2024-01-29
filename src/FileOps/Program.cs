using FileOps.Core;
using FileOps.Core.Features.Parse;
using FileOps.Core.Features.Process;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

using var cancellationTokenRegistration = new CancellationTokenRegistration();
var cancellationToken = cancellationTokenRegistration.Token;

var path = Environment.CurrentDirectory;

var serviceConfiguration = new ServiceCollection().RegisterServices(path, new[] { typeof(Program).Assembly });
var services = serviceConfiguration.BuildServiceProvider();
var mediator = services.GetRequiredService<IMediator>();
var parsedResult = await mediator.Send(new ParseCommand { }, cancellationToken);

var processedResult = await mediator.Send(new ProcessCommand {  }, cancellationToken);