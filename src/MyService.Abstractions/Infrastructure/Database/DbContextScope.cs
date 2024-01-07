namespace MyService.Abstractions.Infrastructure.Database;

public sealed class DbContextScope : IDisposable
{
    private readonly string _shardId;
    private bool _disposedValue;
    private IRelationalDbContext? _relationalDbContext;

    public DbContextScope(string shardId)
    {
        _shardId = shardId;
    }

    public string ShardId => _shardId;

    internal void SetInternalContext(IRelationalDbContext relationalDbContext)
    {
        _relationalDbContext = relationalDbContext;
    }

    internal IRelationalDbContext? GetInternalContext()
    {
        return _relationalDbContext;
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // Dispose internal scope
                GetInternalContext()?.Dispose();
            }
            _disposedValue = true;
        }
    }
}

