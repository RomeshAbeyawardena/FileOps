﻿using FileOps.Core.Features.Process;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Scrutor;
using Shared;
using System.Reflection;

namespace FileOps.Core;

public static class ServiceRegistration
{
    internal static IServiceCollection AddInternalServices(
        this IServiceCollection services, params Assembly[] assemblies)
    {
        return services
            .AddSingleton(TimeProvider.System)
            .AddSingleton<IClockProvider, SystemClockProvider>()
            .AddSingleton<IDirectoryOperation, FileSystemDirectoryOperation>()
            .AddSingleton<IFileOperation, FileSystemOperation>()
            .AddSingleton<IFileProvider, FileSystemProvider>()
            .AddSingleton<IOperationExecutorMapper, OperationExecutorMapper>()
            .Scan(s => s.FromAssemblies(assemblies).AddClasses(a => a.WithAttribute<ServiceDescriptorAttribute>())
            .AsImplementedInterfaces());
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services, 
        IEnumerable<Assembly>? assemblies = null,
        MediatRServiceConfiguration? mediatRServiceConfiguration = null)
    {
        var thisAssembly = typeof(ServiceRegistration).Assembly;
        if (assemblies == null)
        {
            assemblies = new[] {
                thisAssembly
            };
        }
        else
        {
            assemblies = assemblies.Append(thisAssembly);
        }

        if(mediatRServiceConfiguration != null)
        {
            mediatRServiceConfiguration
                .RegisterServicesFromAssembly(thisAssembly);
        }
        else
        {
            services
                .AddInternalServices(assemblies.ToArray())
                .AddMediatR(c => c.RegisterServicesFromAssemblies(
                    assemblies.ToArray()));
        }

        return services;
    }
}
