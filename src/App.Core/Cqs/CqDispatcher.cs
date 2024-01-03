

namespace App.Core.Cqs;

using System.Diagnostics;

[DebuggerStepThrough()]
public class CqDispatcher : IDispatcher
{
    readonly IQueryDispatcherAsync _queryDispatcher;
    readonly ICommandDispatcherAsync _commandDispatcher;

    public CqDispatcher(IQueryDispatcherAsync queryDispatcher, ICommandDispatcherAsync commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    public Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default)
    {
        return _commandDispatcher.DispatchAsync(cmd, cancellationToken);
    }

    public Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default)
    {
        return _commandDispatcher.DispatchAsync(cmd, cancellationToken);
    }

    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return _queryDispatcher.DispatchAsync(query, cancellationToken);
    }
}
