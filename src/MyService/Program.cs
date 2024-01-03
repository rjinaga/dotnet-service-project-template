
/* Build and Run Web Application  */
WebApplication
  .CreateBuilder(args)

  /* Setup DI provider, logger and services */
  .SetupAppAutofacContainer()
  .SetupAppServices()

  .Build()

  /* Setup HTTP request pipeline */
  .SetupAppHttpPipelineConfig()
  .Run();