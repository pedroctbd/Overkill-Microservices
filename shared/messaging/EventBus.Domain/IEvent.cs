namespace EventBus.Domain;

public interface IEvent { }

public interface IAggregateEvent : IEvent
{
    string AggregateName { get; }
    string AggregateKey { get; }
    DateTimeOffset? CreatedAt { get; }
}
