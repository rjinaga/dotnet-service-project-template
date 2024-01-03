﻿namespace MyService.Abstractions.Cqs.CustomerService;

using App.Core.Cqs;
using MyService.Abstractions.Models;


public record GetCustomerQuery(int CustomerId) : IQuery<Customer>
{
}