namespace MyService.Abstractions.Services.CustomerService;

using App.Core.Cqs;
using MyService.Abstractions.Models;

public record CustomerCommand (Customer Customer) : ICommand<int>
{
    
}
