namespace App.Core.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class EventPublisherAsync : IEventPublisherAsync
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof IEvent<TArg> and typeof IEventHandlerAsync<,>
    private readonly ConcurrentDictionary<Type, Type> _cache;

    public EventPublisherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = new ConcurrentDictionary<Type, Type>();
    }

    public async Task PublishAsync<TArg>(IEvent<TArg> @event, CancellationToken token=default) where TArg : class
    {
        Type eventType = @event.GetType();
        Type eventHandlerType;

        if (!_cache.ContainsKey(eventType))
        {
            eventHandlerType = typeof(IEventHandlerAsync<,>).MakeGenericType(eventType, typeof(TArg));
            _ = _cache.TryAdd(eventType, eventHandlerType);
        }
        else
        {
            eventHandlerType = _cache[eventType];
        }
        
        dynamic handler = _context.Resolve(eventHandlerType);
        await handler.HandleAsync(@event.Arg, token);
    }
}
