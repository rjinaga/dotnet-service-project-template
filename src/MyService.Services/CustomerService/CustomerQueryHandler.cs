﻿namespace MyService.Services.CustomerService;

using App.Core;
using App.Core.Cqs;
using MyService.Abstractions.Cqs.CustomerService;
using MyService.Abstractions.Models;
using System.Threading.Tasks;

[Service]
public class CustomerQueryHandler : IQueryHandlerAsync<GetCustomerQuery, Customer>
{
    public Task<Customer> HandleAsync(GetCustomerQuery query)
    {
        return Task.FromResult(new Customer());
    }
}
