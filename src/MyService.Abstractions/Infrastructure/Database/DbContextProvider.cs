namespace MyService.Abstractions.Infrastructure.Database;

public abstract class DbContextProvider
{
    /// <summary>
    /// Creates new db context instance if it does not associated with <paramref name="context"/> and returns values that's
    /// associated with context. Its thread safe with given the object <paramref name="scope"/>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public IRelationalDbContext GetOrCreateDbContext(DbContextScope scope)
    {
        ArgumentNullException.ThrowIfNull(nameof(scope));

        if (scope.GetInternalContext() != null)
        {
            return scope.GetInternalContext()!;
        }

        lock (scope)
        {
            if (scope.GetInternalContext() == null)
            {
                var relationDbContext = Create(scope);
                scope.SetInternalContext(relationDbContext);
            }
        }
        return scope.GetInternalContext()!;
    }
    protected abstract IRelationalDbContext Create(DbContextScope scope);

}

