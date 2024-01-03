namespace App.Core.Cqs;

using Autofac;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class EventPublisherAsync : IEventPublisherAsync
{
    private readonly IComponentContext _context;

    public EventPublisherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task PublishAsync<TArg>(IEvent<TArg> @event) where TArg : class
    {
        var type = typeof(IEventHandlerAsync<,>).MakeGenericType(@event.GetType(),  typeof(TArg));
        dynamic handler = _context.Resolve(type);

        dynamic arg = @event;
        var result = await handler.HandleAsync(arg);
    }
}