namespace App.Core.Cqs;

using Autofac;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class CommandDispatcherAsync : ICommandDispatcherAsync
{
    private readonly IComponentContext _context;

    public CommandDispatcherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
    {
        var type = typeof(ICommandHandlerAsync<,>).MakeGenericType(cmd.GetType(), typeof(TResult));
        dynamic handler = _context.Resolve(type);

        dynamic arg = cmd;
        var result = handler.HandleAsync(arg);

        return result;
    }

    public Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default)
    {
        var type = typeof(ICommandHandlerAsync<>).MakeGenericType(cmd.GetType());
        dynamic handler = _context.Resolve(type);

        dynamic arg = cmd;
        var result = handler.HandleAsync(arg);

        return result;
    }
}