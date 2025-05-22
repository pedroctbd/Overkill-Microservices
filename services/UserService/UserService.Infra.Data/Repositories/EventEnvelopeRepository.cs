using EventBus.Api;

namespace UserService.Infra.Data.Repositories;

public class EventEnvelopeRepository : IEventEnvelopeRepository
{
    private readonly AppDbContext _context;

    public EventEnvelopeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EventEnvelope envelope)
    {
        await _context.EventEnvelopes.AddAsync(envelope);
    }
}
