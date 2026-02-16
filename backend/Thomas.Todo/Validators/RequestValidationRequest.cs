namespace Todo.Validators;

using FluentValidation;

internal sealed class RequestValidationService<TRequest>(TRequest request, IEnumerable<IValidator<TRequest>> validators)
{
    private readonly ValidationContext<TRequest> _context = new(request);

    public void Validate()
    {
        var errors = validators
                .Select(x => x.Validate(_context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }
    }
}