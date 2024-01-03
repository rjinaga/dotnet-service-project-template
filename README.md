# dotnet-service-project-template
.NET project structure/template to create well maintainable, clean and scalable microservice.

This project structure is built using following princples:
Dependency Inversion,
Single Responsibility,
Interface Segregation,
and Open Closed Principle

![Project Dependency Hierarchy](https://raw.githubusercontent.com/rjinaga/dotnet-service-project-template/main/diagram.svg)


## CQS Pattern
This project structure uses CQS pattern to seprate the concerns of actions and retrieving data.
Use command handler to perform an action and query handler for retrieving data.

### Examples for Command, CommandHandler, Event, EventHandler, Query and QueryHandler

#### Command
```csharp
namespace MyService.Abstractions.Cqs.CustomerService;

public record CustomerCommand (Customer Customer) : ICommand<int>
{
    
}
```

#### Command Handler
```csharp
namespace MyService.Services.CustomerService;

[Service]
public class CustomerCommandHandler : ICommandHandlerAsync<CustomerCommand, int>
{
    readonly IEventPublisherAsync _publisher;
    public CustomerCommandHandler(IEventPublisherAsync publisher)
    {
        _publisher = publisher;
    }

    public async Task<int> HandleAsync(CustomerCommand command)
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
    public Task HandleAsync(Customer arg)
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
{
    public Task<Customer> HandleAsync(GetCustomerQuery query)
    {
        throw new NotImplementedException();
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
        CustomerCommand command = new(customer);
        int id = await _dispatcher.DispatchAsync(command);

        return Created("", id);
    }
}
```

#### Container Setup (Check ContainerSetup.cs)
TODO

#### Further enhancement to the structure
* Abstractions module can be seprated by its major folders like Abstractions.Cqs, Abstractions.Infrastructure, Abstractions.Repositories