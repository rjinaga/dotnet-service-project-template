# dotnet-service-project-template
.NET project structure/template to create well maintainable and scalable microservice.

This project structure is built using following princples:
Dependency Inversion,
Single Responsibility,
Interface Segregation,
and Open Closed Principle

![Project Dependency Hierarchy](https://raw.githubusercontent.com/rjinaga/dotnet-service-project-template/main/diagram.svg?token=GHSAT0AAAAAACMKIPHOVVXSKJ5TYHMR2KTEZMVKGWA)


## CQS Pattern
This project structure uses CQS pattern to seprate the concerns of actions and retrieving data.
Use command handler to perform an action and query handler for retrieving data.

### Examples for Command, CommandHandler, Event, EventHandler, Query and QueryHandler

#### Command
```csharp
public record CustomerCommand (Customer Customer) : ICommand<int>
{
    
}
```

#### Command Handler
```csharp
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
public record CustomerCreatedEvent(Customer Arg) : IEvent<Customer>
{
}
```

#### Event Handler
```csharp
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
public record GetCustomerQuery(int CustomerId) : IQuery<Customer>
{
}
```

#### Query Handler
```csharp
[Service]
public class CustomerQueryHandler : IQueryHandlerAsync<GetCustomerQuery, Customer>
{
    public Task<Customer> HandleAsync(GetCustomerQuery query)
    {
        throw new NotImplementedException();
    }
}
```

