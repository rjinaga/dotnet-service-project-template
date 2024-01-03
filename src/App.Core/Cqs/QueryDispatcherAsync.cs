

namespace App.Core.Cqs;

using Autofac;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

[DebuggerStepThrough()]
public class QueryDispatcherAsync : IQueryDispatcherAsync
{
    private readonly IComponentContext _context;

    public QueryDispatcherAsync(IComponentContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<TResult> DispatchAsync<TResult>(IQuery<TResult> cmd, CancellationToken cancellationToken = default)
    {
        var type = typeof(IQueryHandlerAsync<,>).MakeGenericType(cmd.GetType(), typeof(TResult));
        dynamic handler = _context.Resolve(type);

        dynamic arg = cmd;
        var result = handler.HandleAsync(arg);

        return result;
    }
}
