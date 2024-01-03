

namespace App.Core.Cqs;

using System.Threading.Tasks;

public interface IQueryDispatcherAsync
{
    Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}
