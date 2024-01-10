namespace MyService.Abstractions.Services.CustomerService;

using App.Cqs;
using MyService.Abstractions.Models;


public record GetCustomerQuery(int CustomerId) : IQuery<Customer>
{
}