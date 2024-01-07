namespace MyService.Abstractions.Repositories;

using MyService.Abstractions.Infrastructure.Database;
using MyService.Abstractions.Models;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerAsync(int id, DbContextScope scope);
}
