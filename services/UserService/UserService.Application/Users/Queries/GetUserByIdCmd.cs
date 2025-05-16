using FluentValidation;
using MediatR;
using UserService.Application.SeedWork;
using UserService.Domain;
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
    private readonly IUnitOfWork _unitOfWork;
    public GetUserByIdCmdHandler(IUnitOfWork unitOfWork)
    {

        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(GetUserByIdCmd request, CancellationToken cancellationToken)
    {

        return new Response(await _unitOfWork.UserRepository.GetByIdAsync(request.Id));

    }
}

