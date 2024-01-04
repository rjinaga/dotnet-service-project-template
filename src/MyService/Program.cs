
/* Build and Run Web Application  */
WebApplication
  .CreateBuilder(args)

  /* Setup DI provider, services and Build */
  .SetupAppAutofacContainer()
  .SetupAppServices()
  .Build()

  /* Setup HTTP request pipeline and Run */
  .SetupMiddlware()
  .Run();