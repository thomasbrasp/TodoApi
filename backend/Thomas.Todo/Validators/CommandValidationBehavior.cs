namespace Todo.Validators;

using FluentValidation;
using MediatR;


public sealed class CommandValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return next();
        }

        new RequestValidationService<TRequest>(request, validators).Validate();
        return next();
    }
}