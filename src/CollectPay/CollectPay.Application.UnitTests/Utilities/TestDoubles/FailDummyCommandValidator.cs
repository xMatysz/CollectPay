using FluentValidation;
using FluentValidation.Results;

namespace CollectPay.Application.UnitTests.Utilities.TestDoubles;

public class FailDummyCommandValidator : IValidator<DummyCommand>
{
	public ValidationResult Validate(IValidationContext context)
	{
		return default!;
	}

	public Task<ValidationResult> ValidateAsync(IValidationContext context, CancellationToken cancellation = new CancellationToken())
	{
		return default!;
	}

	public IValidatorDescriptor CreateDescriptor()
	{
		return default!;
	}

	public bool CanValidateInstancesOfType(Type type)
	{
		return default!;
	}

	public ValidationResult Validate(DummyCommand instance)
	{
		return default!;
	}

	public Task<ValidationResult> ValidateAsync(DummyCommand instance, CancellationToken cancellation = new CancellationToken())
	{
		var validationFail = new ValidationFailure("TestProperty", "TestMessage");
		return Task.FromResult(new ValidationResult(new[] { validationFail }));
	}
}