using FluentValidation;
using MediatR;
using UserService.Application.SeedWork;

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
    public GetUserByIdCmdHandler()
    {

    }

    public async Task<Response> Handle(GetUserByIdCmd request, CancellationToken cancellationToken)
    {

        return null;
    }
}

