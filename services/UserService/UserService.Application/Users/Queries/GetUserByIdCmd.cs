using FluentValidation;
using MediatR;
using UserService.Application.SeedWork;
using UserService.Domain.Users;

namespace UserService.Application.Users.Queries;


public record GetUserByIdCmd(string Id) : IRequest<Response>;

public class GetUserByIdCmdValidator : AbstractValidator<GetUserByIdCmd>
{
    public GetUserByIdCmdValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
    }
}

public class GetUserByIdCmdHandler : IRequestHandler<GetUserByIdCmd, Response>
{
    private readonly IUserRepository _userRepository;
    public GetUserByIdCmdHandler(IUserRepository userRepository)
    {
        
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(GetUserByIdCmd request, CancellationToken cancellationToken)
    {

        return new Response(await _userRepository.GetByIdAsync(request.Id));

    }
}

