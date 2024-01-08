namespace MyService.Infrastructure.Database;

using App.Core;
using Microsoft.EntityFrameworkCore;
using MyService.Abstractions.Entities;
using MyService.Abstractions.Infrastructure.Database;

[Service]
public class MyServiceSqlServerDbContext : DbContext, IRelationalDbContext
{
    readonly string _connectionString;
    public MyServiceSqlServerDbContext(string connectionString) {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>()
                    .ToTable("Customer");
    }
}
