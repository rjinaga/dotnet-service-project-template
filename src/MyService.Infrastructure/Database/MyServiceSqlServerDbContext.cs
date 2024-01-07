namespace MyService.Infrastructure.Database;

using App.Core;
using Microsoft.EntityFrameworkCore;
using MyService.Abstractions.Infrastructure.Database;

[Service]
public class MyServiceSqlServerDbContext : DbContext, IRelationalDbContext
{
    public MyServiceSqlServerDbContext(string connectionString) { }
}
