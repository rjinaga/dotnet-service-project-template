namespace App.Core.Cqs;

public interface IEventPublisherAsync
{
    Task PublishAsync<T>(IEvent<T> @event, CancellationToken token=default) where T: class;
}
