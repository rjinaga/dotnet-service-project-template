namespace MyService.Infrastructure.Database;

using MyService.Abstractions.Infrastructure.Database;

public class MyServiceDbContextProvider : DbContextProvider
{
    readonly string _connectionString;
    public MyServiceDbContextProvider(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException($"{nameof(connectionString)} cannot be null or empty.");

        _connectionString = connectionString;
    }

    protected override IRelationalDbContext Create(DbContextScope context)
    {
        return new MyServiceSqlServerDbContext(_connectionString);
    }
}
