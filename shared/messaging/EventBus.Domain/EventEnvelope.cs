namespace EventBus.Api;


public class EventEnvelope
{
    public string Id { get; set; }
    public string Payload { get; set; }
    public string EventType { get; set; }
    public string AggregateId { get; set; }
    public string AggregateType { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Source { get; set; }
    public Dictionary<string, string> Metadata { get; set; }

    public EventEnvelope(
          string payload,
          string eventType,
          string aggregateId,
          string aggregateType,
          string source = null,
          Dictionary<string, string>? metadata = null)
    {
        Id = Ulid.NewUlid().ToString();
        Payload = payload;
        CreatedAt = DateTimeOffset.UtcNow;
        EventType = eventType;
        AggregateId = aggregateId;
        AggregateType = aggregateType;
        Source = source;
        Metadata = metadata ?? new Dictionary<string, string>();
    }

}


