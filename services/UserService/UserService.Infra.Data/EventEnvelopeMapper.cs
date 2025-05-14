using EventBus.Api;
using EventBus.Domain;
using System.Text.Json;

namespace UserService.Infra.Data;

public static class EventEnvelopeMapper
{
    public static EventEnvelopeEntity ToEntity<T>(EventEnvelope<T> envelope) where T : IEvent
    {
        return new EventEnvelopeEntity
        {
            EventId = envelope.EventId,
            CreatedAt = envelope.CreatedAt,
            EventType = envelope.EventType,
            TraceId = envelope.TraceId,
            InitiatorId = envelope.InitiatorId,
            PartitionId = envelope.PartitionId,
            MetadataJson = JsonSerializer.Serialize(envelope.Metadata),
            PayloadJson = JsonSerializer.Serialize(envelope.Payload)
        };
    }
}
