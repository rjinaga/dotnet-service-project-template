namespace MyService.Abstractions.Services.CustomerService;

using App.Cqs;
using MyService.Abstractions.Models;

public record CreateCustomerCommand (Customer Customer) : ICommand<int>
{
    
}
