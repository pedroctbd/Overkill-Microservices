using FluentValidation;
using MediatR;
using UserService.Application.SeedWork;
using UserService.Domain;

namespace UserService.Application.User.Queries;


public record GetUserByIdCmd(string Id) : IRequest<Response>;

public class GetUserByIdCmdValidator : AbstractValidator<GetUserByIdCmd>
{
    public GetUserByIdCmdValidator()
    {
    }
}

public class GetUserByIdCmdHandler : IRequestHandler<GetUserByIdCmd, Response>
{
    private readonly IUserRepository userRepository;
    public GetUserByIdCmdHandler(IUserRepository _userRepository)
    {

    }

    public async Task<Response> Handle(GetUserByIdCmd request, CancellationToken cancellationToken)
    {

        return new Response(await userRepository.GetByIdAsync(request.Id));

    }
}

