namespace MyService.Services.CustomerService;

using App.Cqs;
using App.Cqs;
using MyService.Abstractions.Services.CustomerService;
using MyService.Abstractions.Models;
using System.Threading.Tasks;
using MyService.Abstractions.Repositories;
using MyService.Abstractions.Infrastructure.Database;

[Service]
public class CustomerQueryHandler : IQueryHandlerAsync<GetCustomerQuery, Customer>
{
    readonly ICustomerRepository _customerRepository;
    public CustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<Customer> HandleAsync(GetCustomerQuery query, CancellationToken token = default)
    {
        using var scope = new DbContextScope("0");
        return _customerRepository.GetCustomerAsync(query.CustomerId, scope);
    }
}
