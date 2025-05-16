using EventBus.Api;
using UserService.Domain.Users;

namespace UserService.Domain;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    IUserRepository UserRepository { get; }
    IEventEnvelopeRepository EventEnvelopeRepository { get; }
}
