
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using App.Core;
using App.Core.Cqs;
using MyService.Infrastructure;
using MyService.Repository;
using MyService.Services;


internal static class ContainerSetup
{
    internal static WebApplicationBuilder SetupAppAutofacContainer(this WebApplicationBuilder builder)
    {
       
        builder.Host
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(cb => Config(cb, builder.Configuration));

        return builder;
    }

    private static void Config(ContainerBuilder builder, ConfigurationManager configuration)
    {
        // :Register services from different assemblies
        RegisterAssembly(builder, typeof(ServicesModule).Assembly);
        RegisterAssembly(builder, typeof(RepositoriesModule).Assembly);
        RegisterAssembly(builder, typeof(InfrastructureModule).Assembly);

        // :Register CQS driver classes
        builder.RegisterType<QueryDispatcherAsync>().As<IQueryDispatcherAsync>().SingleInstance();
        builder.RegisterType<CommandDispatcherAsync>().As<ICommandDispatcherAsync>().SingleInstance();
        builder.RegisterType<CqDispatcher>().As<IDispatcher>().SingleInstance();
        builder.RegisterType<EventPublisherAsync>().As<IEventPublisherAsync>().SingleInstance();
    }

    private static void RegisterAssembly(ContainerBuilder builder, Assembly assembly)
    {
        // :Register transient services
        builder.RegisterAssemblyTypes(assembly)
           .Where(t =>
           {
               var sa = t.GetCustomAttribute<ServiceAttribute>();
               return sa != null && sa.InstanceLifetime == InstanceLifetime.Transient;
           })
           .AsImplementedInterfaces();

        // :Register singleton services
        builder.RegisterAssemblyTypes(assembly)
           .Where(t =>
           {
               var sa = t.GetCustomAttribute<ServiceAttribute>();
               return sa != null && sa.InstanceLifetime == InstanceLifetime.Singleton;
           })
           .AsImplementedInterfaces()
           .SingleInstance();
    }
}
