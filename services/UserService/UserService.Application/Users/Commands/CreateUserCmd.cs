using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.SeedWork;
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
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    public CreateUserCmdHandler(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
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

        await _userRepository.AddAsync(newUser);

        return new Response(newUser);
    }
}


