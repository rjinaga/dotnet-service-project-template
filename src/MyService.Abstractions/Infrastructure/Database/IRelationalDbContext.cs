namespace MyService.Abstractions.Infrastructure.Database;

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public interface IRelationalDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
