using EventBus.Api;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.SeedWork;
using UserService.Domain;
using UserService.Domain.Users;

namespace UserService.Application.Users.Commands;

public record CreateUserCmd(string Name, string Email, string Password, string Role = "User") : IRequest<Response>;
public class CreateUserCmdValidator : AbstractValidator<CreateUserCmd>
{
    public CreateUserCmdValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters.");
    }
}

public class CreateUserCmdHandler : IRequestHandler<CreateUserCmd, Response>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher<User> _passwordHasher;
    public CreateUserCmdHandler(IUnitOfWork unitOfWork, IPasswordHasher<User> passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<Response> Handle(CreateUserCmd request, CancellationToken cancellationToken)
    {
        var newUser = new User();
        var passwordHash = _passwordHasher.HashPassword(newUser, request.Password);

        newUser.Name = request.Name;
        newUser.Email = request.Email;
        newUser.PasswordHash = passwordHash;
        newUser.Role = request.Role;

        await _unitOfWork.UserRepository.AddAsync(newUser);

        var userEvent = new UserCreatedEvent( newUser.Id ) { CreatedAt = DateTimeOffset.Now };

        var kafkaEvent = new EventEnvelope<UserCreatedEvent>
        (
           userEvent
        );

        await _unitOfWork.EventEnvelopeRepository.AddAsync(kafkaEvent);
        await _unitOfWork.SaveChangesAsync();
        return new Response(newUser);
    }
}


