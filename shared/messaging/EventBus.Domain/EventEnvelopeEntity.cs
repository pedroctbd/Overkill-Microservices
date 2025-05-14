using System.ComponentModel.DataAnnotations;

namespace EventBus.Domain;

public class EventEnvelopeEntity
{
    [Key]
    public string EventId { get; set; } = default!;
    public string PayloadJson { get; set; } = default!;
    public string EventType { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public string? TraceId { get; set; }
    public string InitiatorId { get; set; } = default!;
    public string? PartitionId { get; set; }
    public string MetadataJson { get; set; } = "{}";
}
