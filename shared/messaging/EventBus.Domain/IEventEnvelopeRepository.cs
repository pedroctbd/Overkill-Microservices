using EventBus.Domain;

namespace EventBus.Api;

public interface IEventEnvelopeRepository
{
    Task AddAsync<T>(EventEnvelope<T> envelope) where T : IEvent;
}
