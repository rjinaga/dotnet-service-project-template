namespace App.Core.Cqs;

using Autofac;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class CommandDispatcherAsync : ICommandDispatcherAsync
{
    private readonly IComponentContext _context;

    // Maintain dictionary of typeof ICommand<TResult> or ICommand and typeof ICommandHandlerAsync<,> key value pair
    private readonly ConcurrentDictionary<Type, Type> _cache;

    public CommandDispatcherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = new ConcurrentDictionary<Type, Type>();
    }

    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cache.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandlerAsync<,>).MakeGenericType(cmdType, typeof(TResult));
            _ = _cache.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cache[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        var taskResult = handler.HandleAsync(arg, cancellationToken);

        return taskResult;
    }

    public Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default)
    {
        Type cmdType = cmd.GetType();
        Type cmdHandlerType;

        if (!_cache.ContainsKey(cmdType))
        {
            cmdHandlerType = typeof(ICommandHandlerAsync<>).MakeGenericType(cmdType);
            _ = _cache.TryAdd(cmdType, cmdHandlerType);
        }
        else
        {
            cmdHandlerType = _cache[cmdType];
        }

        dynamic handler = _context.Resolve(cmdHandlerType);
        dynamic arg = cmd;
        var taskResult = handler.HandleAsync(arg, cancellationToken);

        return taskResult;
    }
}