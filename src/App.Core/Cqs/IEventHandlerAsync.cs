namespace App.Core.Cqs;

using System.Threading.Tasks;

public interface IEventHandlerAsync<TEvent, TArg> 
    where TEvent : IEvent<TArg> 
    where TArg : class
{
    Task HandleAsync(TArg arg);
}
