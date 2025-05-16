using EventBus.Api;
using EventBus.Domain;

namespace UserService.Infra.Data.Repositories;

public class EventEnvelopeRepository : IEventEnvelopeRepository
{
    private readonly AppDbContext _context;

    public EventEnvelopeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(EventEnvelope<T> envelope) where T : IEvent
    {
        var entity = EventEnvelopeMapper.ToEntity(envelope);
        await _context.EventEnvelopes.AddAsync(entity);
    }
}
