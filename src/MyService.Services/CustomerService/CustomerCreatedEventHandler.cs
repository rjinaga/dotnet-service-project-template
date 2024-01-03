namespace MyService.Services.CustomerService;

using App.Core;
using App.Core.Cqs;
using MyService.Abstractions.Cqs.CustomerService;
using MyService.Abstractions.Models;
using System;
using System.Threading.Tasks;

[Service]
public class CustomerCreatedEventHandler : IEventHandlerAsync<CustomerCreatedEvent, Customer>
{
    public Task HandleAsync(Customer arg)
    {
        throw new NotImplementedException();
    }
}
