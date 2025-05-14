using EventBus.Api;
using EventBus.Domain;

namespace UserService.Infra.Data.Repositories;

public class EventEnvelopeRepository : IEventEnvelopeRepository
{
    private readonly EventEnvelopeDbContext _context;

    public EventEnvelopeRepository(EventEnvelopeDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(EventEnvelope<T> envelope) where T : IEvent
    {
        var entity = EventEnvelopeMapper.ToEntity(envelope);
        _context.EventEnvelopes.Add(entity);
        await _context.SaveChangesAsync();
    }
}
