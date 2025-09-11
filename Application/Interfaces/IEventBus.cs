namespace Application.Interfaces;

public interface IEventBus<TMessage> : IDisposable
{
    Task PublishAsync(TMessage message,  CancellationToken ct);
}