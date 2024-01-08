namespace App.Core;

using App.Core.Cqs;
using Autofac;
using System.Linq;
using System.Reflection;


public static class ContainerBuilderExtensions
{
    public static void RegisterCqs(this ContainerBuilder builder)
    {
        builder.RegisterType<QueryDispatcherAsync>().As<IQueryDispatcherAsync>().SingleInstance();
        builder.RegisterType<CommandDispatcherAsync>().As<ICommandDispatcherAsync>().SingleInstance();
        builder.RegisterType<CqDispatcher>().As<IDispatcher>().SingleInstance();
        builder.RegisterType<EventPublisherAsync>().As<IEventPublisherAsync>().SingleInstance();
    }

    public static void RegisterAssemblyTypesWithServiceAttr(this ContainerBuilder builder, Assembly assembly)
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
