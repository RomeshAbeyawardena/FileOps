using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Scrutor;
using System.Reflection;

namespace FileOps.Core;

public static class ServiceRegistration
{
    internal static IServiceCollection AddInternalServices(this IServiceCollection services, string rootPath, params Assembly[] assemblies)
    {
        return services
            .AddSingleton<IDirectoryOperation, FileSystemDirectoryOperation>()
            .AddSingleton<IFileOperation, FileSystemOperation>()
            .AddSingleton<IFileProvider, PhysicalFileProvider>(s => new PhysicalFileProvider(rootPath))
            .Scan(s => s.FromAssemblies(assemblies).AddClasses(a => a.WithAttribute<ServiceDescriptorAttribute>())
            .AsImplementedInterfaces());
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services, 
        string rootPath,
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
            mediatRServiceConfiguration.RegisterServicesFromAssembly(thisAssembly);
        }
        else
        {
            services
                .AddInternalServices(rootPath, assemblies.ToArray())
                .AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies.ToArray()));
        }

        return services;
    }
}
