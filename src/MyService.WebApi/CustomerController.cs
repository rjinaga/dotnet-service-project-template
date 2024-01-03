namespace MyService.WebApi;

using App.Core.Cqs;
using Microsoft.AspNetCore.Mvc;
using MyService.Abstractions.Cqs.CustomerService;
using MyService.Abstractions.Models;
using System.Threading.Tasks;

public class CustomerController : ControllerBase
{
    readonly IDispatcher _dispatcher;
    public CustomerController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int customerId)
    {
        GetCustomerQuery query = new (customerId);
        Customer customer = await _dispatcher.DispatchAsync(query);

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer customer)   
    {
        CustomerCommand command = new(customer);
        int id = await _dispatcher.DispatchAsync(command);

        return Created("", id);
    }
}
