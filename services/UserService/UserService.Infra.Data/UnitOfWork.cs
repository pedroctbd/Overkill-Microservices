using EventBus.Api;
using UserService.Domain;
using UserService.Domain.Users;
using UserService.Infra.Data.Repositories;

namespace UserService.Infra.Data;

public class UnitOfWork : IUnitOfWork
{

    private AppDbContext _context;
    private IUserRepository _userRepository;
    private IEventEnvelopeRepository _eventEnvelopeRepository;


    public UnitOfWork(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

    public IEventEnvelopeRepository EventEnvelopeRepository => _eventEnvelopeRepository ??= new EventEnvelopeRepository(_context);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

