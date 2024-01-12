# dotnet-service-project-template
.NET project structure or template to create well maintainable, clean and scalable microservice. With this structure, entire hierarchy of objects can be developed as stateless. This project structure can be used by replacing your desired name for your service. 

This project structure is built using following principles:
* Dependency Inversion - High or low level modules don't depend on each other, all the concrete modules depend on abstraction module, and host module is responsible for assembling all the concrete modules.
* Single Responsibility - This project structure uses Cqs pattern at its core, so individual actions or queries are clearly serrated by its functionality.
* Interface Segregation - Command and query separation pattern allows to use the client specific interfaces.
* Open Closed - Event publisher makes the command or query classes closed for modifications and open for extension by adding events.


### Modules Dependency

![Project Dependency Hierarchy](https://raw.githubusercontent.com/rjinaga/dotnet-service-project-template/main/diagram.svg)


## CQS Pattern
This project structure uses CQS pattern to seprate the concerns of actions and retrieving data.
Use command handler to perform an action and query handler for retrieving data.

### Examples for Command, CommandHandler, Event, EventHandler, Query and QueryHandler

#### Command
```csharp
namespace MyService.Abstractions.Cqs.CustomerService;

public record CreateCustomerCommand (Customer Customer) : ICommand<int>
{
    
}
```

#### Command Handler
```csharp
namespace MyService.Services.CustomerService;

[Service]
public class CustomerCommandHandler : ICommandHandlerAsync<CustomerCommand, int>
{
    readonly IEventPublisher _publisher;
    public CustomerCommandHandler(IEventPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task<int> HandleAsync(CreateCustomerCommand command, CancellationToken token = default)
    {
        var customer = command.Customer;

        // TODO: create customer record
        
        await _publisher.PublishAsync(new CustomerCreatedEvent(customer));
        return customer.Id;
    }
}
```


#### Event
```csharp
namespace MyService.Abstractions.Cqs.CustomerService;

public record CustomerCreatedEvent(Customer Arg) : IEvent<Customer>
{
}
```

#### Event Handler
```csharp
namespace MyService.Services.CustomerService;

[Service]
public class CustomerCreatedEventHandler : IEventHandlerAsync<CustomerCreatedEvent, Customer>
{
    public Task HandleAsync(Customer arg, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}
```

#### Query 
```csharp
namespace MyService.Abstractions.Cqs.CustomerService;
public record GetCustomerQuery(int CustomerId) : IQuery<Customer>
{
}
```

#### Query Handler
```csharp
namespace MyService.Services.CustomerService;

[Service]
public class CustomerQueryHandler : IQueryHandlerAsync<GetCustomerQuery, Customer> 
//, IQueryHandlerAsync<GetCustomerListQuery, IList<Customer>>  // can be implemented multiple interfaces
{
    readonly ICustomerRepository _customerRepository;
    public CustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<Customer> HandleAsync(GetCustomerQuery query, CancellationToken token = default)
    {
        using var scope = new DbContextScope("0");
        return _customerRepository.GetCustomerAsync(query.CustomerId, scope);
    }
}
```

#### Repository Implementation
```csharp
namespace MyService.Repositories;
...

[Service]
public class CustomerRepository : ICustomerRepository
{
    readonly DbContextProvider _dbContextProvider;
    public CustomerRepository(DbContextProvider dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public Task<Customer> GetCustomerAsync(int id, DbContextScope scope)
    {
        var dbContext = _dbContextProvider.GetOrCreateDbContext(scope);

        // TODO: use db context to retrieve the data from generic set method
        // use dbContext.Set<CustomerEntity>() as your db set object to perform CRUD operations

        //var customer = await dbContext.Set<CustomerEntity>().Where(c => c.Id = id ).SingleOrDefaultAsync();
        //return customer;

        return Task.FromResult ( new Customer() );
    }
}
```

#### Invoke Command/Query

```csharp
public class CustomerController : ControllerBase
{
    readonly IDispatcher _dispatcher;
    public CustomerController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int customerId)
    {
        GetCustomerQuery query = new (customerId);
        Customer customer = await _dispatcher.DispatchAsync(query);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)   
    {
        CreateCustomerCommand command = new(customer);
        int id = await _dispatcher.DispatchAsync(command);

        return Created("", id);
    }
}
```

#### Container Setup (Check ContainerSetup.cs)
<< TODO >>

#### Further enhancement to the structure
* Abstractions module can be seprated by its major folders like Abstractions.Cqs, Abstractions.Infrastructure, Abstractions.Repositories

#### Project Structure

![Project Structure](https://raw.githubusercontent.com/rjinaga/dotnet-service-project-template/main/project-structure.png)


#### Project Dependencies
* ASP.NET Core 6.0
* Autofac
* [App.Cqs](https://github.com/rjinaga/App.Cqs)  
[![NuGet Version](https://img.shields.io/nuget/v/App.Cqs)](https://www.nuget.org/packages/App.Cqs)

#### Roadmap
* Create shell script to accepting few arguments to create a new project from this as a real template.
* Unit testing and integration testing
* Improve documentation
