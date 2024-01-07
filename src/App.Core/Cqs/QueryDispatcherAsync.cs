namespace App.Core.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class QueryDispatcherAsync : IQueryDispatcherAsync
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof IQuery<TResult> and typeof IQueryHandlerAsync<,> key value pair
    private readonly ConcurrentDictionary<Type, Type> _cache;

    public QueryDispatcherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = new ConcurrentDictionary<Type, Type>();
    }

    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        Type queryType = query.GetType();
        Type queryHandlerType;

        if (!_cache.ContainsKey(queryType))
        {
            queryHandlerType = typeof(IQueryHandlerAsync<, >).MakeGenericType(queryType, typeof(TResult));
            _ = _cache.TryAdd(queryType, queryHandlerType);
        }
        else
        {
            queryHandlerType = _cache[queryType];
        }

        dynamic handler = _context.Resolve(queryHandlerType);
        dynamic arg = query;
        var result = handler.HandleAsync(arg, cancellationToken);

        return result;
    }
}
