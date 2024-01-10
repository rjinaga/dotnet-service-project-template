namespace MyService.Services.CustomerService;

using App.Cqs;
using App.Cqs;
using MyService.Abstractions.Services.CustomerService;
using MyService.Abstractions.Models;
using System.Threading.Tasks;

[Service]
public class CustomerCreatedEventHandler : IEventHandlerAsync<CustomerCreatedEvent, Customer>
{
    public Task HandleAsync(Customer arg, CancellationToken token = default)
    {
        return Task.CompletedTask;
    }
}
