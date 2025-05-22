namespace EventBus.Api;

public interface IEventEnvelopeRepository
{
    Task AddAsync(EventEnvelope envelope);
}
