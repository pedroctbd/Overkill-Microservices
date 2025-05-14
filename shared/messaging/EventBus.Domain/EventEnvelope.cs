using EventBus.Domain;

namespace EventBus.Api;

public interface IEventEnvelope<out T> where T : IEvent
{
    public string EventId { get; }
    public T Payload { get; }
    public DateTimeOffset CreatedAt { get; }
    public string? TraceId { get; }
    public string InitiatorId { get; }
    public string EventType { get; }
    public string? PartitionId { get; }
    public Dictionary<string, string> Metadata { get; }
}

public class EventEnvelope<T> : IEventEnvelope<T> where T : IEvent
{
    public string EventId { get; set; }
    public T Payload { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? TraceId { get; set; }
    public string InitiatorId { get; set; }
    public string EventType { get; set; }
    public string? PartitionId { get; set; }
    public Dictionary<string, string> Metadata { get; set; }

    public EventEnvelope(
          T payload,
          string? partitionId = null,
          string? traceId = null,
          string? initiatorId = null,
          Dictionary<string, string>? metadata = null)
    {
        EventId = Ulid.NewUlid().ToString();
        Payload = payload;
        CreatedAt = DateTimeOffset.UtcNow;
        TraceId = traceId;
        InitiatorId = initiatorId ?? "system";
        EventType = typeof(T).AssemblyQualifiedName!;
        PartitionId = partitionId;
        Metadata = metadata ?? new Dictionary<string, string>();
    }

    public EventEnvelope()
    {
    }
}


