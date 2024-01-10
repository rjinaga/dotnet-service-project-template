
using Autofac;
using Autofac.Extensions.DependencyInjection;
using App.Cqs;
using MyService.Infrastructure;
using MyService.Repository;
using MyService.Services;
using MyService.Abstractions.Infrastructure.Database;
using MyService.Infrastructure.Database;

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

        builder.RegisterAssemblyTypesWithServiceAttr(typeof(ServicesModule).Assembly);
        builder.RegisterAssemblyTypesWithServiceAttr(typeof(RepositoriesModule).Assembly);
        builder.RegisterAssemblyTypesWithServiceAttr(typeof(InfrastructureModule).Assembly);

        // :Register CQS driver classes
        builder.RegisterCqs();

        // :Register db context provider
        RegisterDbContextProvider(builder);
    }

    private static void RegisterDbContextProvider(ContainerBuilder builder)
    {
        builder.Register(c => new MyServiceDbContextProvider(connectionString: "-- fetch from app settings--"))
               .As<DbContextProvider>();
    }

}
