
internal static class MiddlewareSetup
{
    internal static WebApplication SetupAppHttpPipelineConfig(this WebApplication app)
    {
        // Middleware to handle and log unhandled exceptions
        // app.UseMiddleware<ExceptionMiddleware>();

        //Middleware to redirect request to HTTPS if a request is made with HTTP
        app.UseHttpsRedirection();
        app.UseHsts();

        // Swagger middleware to handle /swagger related endpoints
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Middleware to validate cross origin requests
        app.UseCors();

        //Register authentication and authorization middleware
        //app.UseMiddleware<AuthenticationMiddleware>()
        //   .UseMiddleware<AuthorizationMiddleware>();

        // Middleware to map appropriate controllers
        app.MapControllers();
        return app;
    }
}
