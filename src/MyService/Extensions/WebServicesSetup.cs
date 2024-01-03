using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using MyService.WebApi;

internal static class WebServicesSetup
{
    /// <summary>
    /// Add controllers, other services to DI
    /// </summary>
    /// <param name="builder"></param>
    internal static WebApplicationBuilder SetupAppServices(this WebApplicationBuilder builder)
    {
        // set lower case URLs
        builder.Services.AddRouting(option => option.LowercaseUrls= true );

        // Add controllers to the DI container
        builder.Services
            .AddControllers()
            .AddApplicationPart(typeof(ControllersModule).Assembly);

        // Add Swagger services to the DI container    
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(ConfigureSwagger);

        // Add CORS
        SetupCorsPolicy(builder);

        return builder;
    }

    private static void ConfigureSwagger(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Service API", Version = "v1" });
    }

    private static void SetupCorsPolicy(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddCors(options =>
        {
            var hosts = builder.Configuration.GetValue<string[]>("AppCors");
            if (hosts == null)
            {
                // TODO: Logging
                return;
            }

            options.AddDefaultPolicy(policy =>
                       policy.WithOrigins(origins: hosts));
        });
    }
}
