

namespace App.Core.Cqs;

using System.Threading.Tasks;

public interface ICommandDispatcherAsync
{
    Task<TResult> DispatchAsync<TResult>(ICommand<TResult> cmd, CancellationToken cancellationToken = default);
    Task DispatchAsync(ICommand cmd, CancellationToken cancellationToken = default);
}