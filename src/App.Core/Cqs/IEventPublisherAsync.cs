namespace App.Core.Cqs;

public interface IEventPublisherAsync
{
    Task PublishAsync<T>(IEvent<T> @event) where T: class;
}
