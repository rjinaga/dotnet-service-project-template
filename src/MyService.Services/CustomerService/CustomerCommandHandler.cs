namespace MyService.Services.CustomerService;

using App.Cqs;
using App.Cqs;
using MyService.Abstractions.Services.CustomerService;
using System.Threading.Tasks;


[Service]
public class CustomerCommandHandler : ICommandHandlerAsync<CreateCustomerCommand, int>
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
