namespace MyService.Repositories;

using App.Core;
using MyService.Abstractions.Infrastructure.Database;
using MyService.Abstractions.Models;
using MyService.Abstractions.Repositories;
using System.Threading.Tasks;

[Service]
public class CustomerRepository : ICustomerRepository
{
    readonly DbContextProvider _dbContextProvider;
    public CustomerRepository(DbContextProvider dbContextProvider)
    {
        _dbContextProvider = dbContextProvider;
    }

    public Task<Customer> GetCustomerAsync(int id, DbContextScope scope)
    {
        var dbContext = _dbContextProvider.GetOrCreateDbContext(scope);

        // TODO
        // dbContext.Set<Customer>().Where(c => c.Id = id );

        return Task.FromResult ( new Customer() );
    }
}
