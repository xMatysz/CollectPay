using System.Reflection;
using CollectPay.Application.Common.Abstraction;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CollectPay.Application.Behaviors;

public class ValidationBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
	where TCommand : ICommand<TResult>
	where TResult : IErrorOr
{
	private readonly IEnumerable<IValidator<TCommand>> _validators;

	public ValidationBehavior(IEnumerable<IValidator<TCommand>> validators)
	{
		_validators = validators;
	}

	public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
	{
		if (!_validators.Any())
		{
			return await next();
		}

		var validationResults = new List<ValidationResult>();

		foreach (var validator in _validators)
		{
			var result = await validator.ValidateAsync(request, cancellationToken);
			validationResults.Add(result);
		}

		if (validationResults.All(x=>x.IsValid))
		{
			return await next();
		}

		return TryCreateResponseFromErrors(validationResults.SelectMany(x=>x.Errors).ToList(), out var response)
			? response
			: throw new ValidationException(validationResults.SelectMany(x=>x.Errors));
	}

	private static bool TryCreateResponseFromErrors(List<ValidationFailure> validationFailures, out TResult response)
	{
		var errors = validationFailures.ConvertAll(x => Error.Validation(
			code: x.PropertyName,
			description: x.ErrorMessage));

		response = (TResult?)typeof(TResult)
			.GetMethod(
				name: nameof(ErrorOr<object>.From),
				bindingAttr: BindingFlags.Static | BindingFlags.Public,
				types: [typeof(List<Error>)])
			?.Invoke(null, [errors]);

		return response is not null;
	}
}