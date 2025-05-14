using EventBus.Domain;

namespace UserService.Domain.Users;


public interface IUserEvent : IAggregateEvent
{

}

public record UserCreatedEvent(string Id) : IUserEvent
{
    public string AggregateKey => Id;
    public string AggregateName => "User";
    public DateTimeOffset? CreatedAt { get; set; }
}
