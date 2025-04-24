using FluentValidation;
using MediatR;

namespace UserService.Application.SeedWork;

public class FailFastBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response
{
    private readonly IValidator _validator;

    public FailFastBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
            return await next();

        var result = await _validator.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);
        if (result.IsValid)
            return await next();

        var response = Activator.CreateInstance<TResponse>();
        foreach (var error in result.Errors)
            response.AddError(error.PropertyName, error.ErrorMessage);

        return response;
    }
}

