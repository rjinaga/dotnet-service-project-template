namespace MyService.Abstractions.Services.CustomerService;

using App.Cqs;
using MyService.Abstractions.Models;

public record CustomerCreatedEvent(Customer Arg) : IEvent<Customer>
{
}
