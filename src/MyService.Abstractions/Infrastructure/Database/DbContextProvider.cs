namespace MyService.Abstractions.Infrastructure.Database;

public abstract class DbContextProvider
{
    /// <summary>
    /// Creates new db context instance if it does not associated with <paramref name="context"/> and returns values that's
    /// associated with context
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public IRelationalDbContext GetOrCreateDbContext(DbContextScope scope)
    {
        ArgumentNullException.ThrowIfNull(nameof(scope));
        
        if (scope.GetInternalContext() == null)
        {
            var relationDbContext = Create(scope);
            scope.SetInternalContext(relationDbContext);
        }

        return scope.GetInternalContext()!;
    }
    protected abstract IRelationalDbContext Create(DbContextScope scope);
    
}

