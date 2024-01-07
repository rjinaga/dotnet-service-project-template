namespace MyService.Services.CustomerService;

using App.Core;
using App.Core.Cqs;
using MyService.Abstractions.Cqs.CustomerService;
using System.Threading.Tasks;


[Service]
public class CustomerCommandHandler : ICommandHandlerAsync<CustomerCommand, int>
{
    readonly IEventPublisherAsync _publisher;
    public CustomerCommandHandler(IEventPublisherAsync publisher)
    {
        _publisher = publisher;
    }

    public async Task<int> HandleAsync(CustomerCommand command, CancellationToken token = default)
    {
        var customer = command.Customer;

        // TODO: create customer record
        
        await _publisher.PublishAsync(new CustomerCreatedEvent(customer));
        return customer.Id;
    }
}
