using FluentValidation;
using MediatR;
using UserService.Application.SeedWork;

namespace UserService.Application.User.Commands;

public record CreateUserCmd() : IRequest<Response>;

public class CreateUserCmdValidator : AbstractValidator<CreateUserCmd>
{
    public CreateUserCmdValidator()
    {
    }
}

public class CreateUserCmdHandler : IRequestHandler<CreateUserCmd, Response>
{
    public CreateUserCmdHandler()
    {

    }

    public async Task<Response> Handle(CreateUserCmd request, CancellationToken cancellationToken)
    {

        return null;
    }
}

