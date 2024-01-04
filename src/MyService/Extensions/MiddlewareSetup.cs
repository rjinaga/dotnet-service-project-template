
internal static class MiddlewareSetup
{
    internal static WebApplication SetupMiddlware(this WebApplication app)
    {

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
        
        // Middleware to map appropriate controllers
        app.MapControllers();
        return app;
    }
}
