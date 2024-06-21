using CollectPay.Application.Behaviors;
using CollectPay.Application.Tests.Unit.Doubles;
using FluentValidation;
using FluentValidation.Results;

namespace CollectPay.Application.Tests.Unit.BehaviorsTests;

public class WhenProcessingValidationBehavior
{
	private readonly ValidationBehavior<DummyCommand, ErrorOr<IErrorOr>> _behavior;
	private readonly RequestHandlerDelegate<ErrorOr<IErrorOr>> _nextDelegate;
	private readonly IValidator<DummyCommand> _validator;
	private readonly DummyCommand _dummyCommand;

	public WhenProcessingValidationBehavior()
	{
		_dummyCommand = new DummyCommand();
		_validator = Substitute.For<IValidator<DummyCommand>>();
		_nextDelegate = Substitute.For<RequestHandlerDelegate<ErrorOr<IErrorOr>>>();
		_behavior = new ValidationBehavior<DummyCommand, ErrorOr<IErrorOr>>([_validator]);
	}

	[Fact]
	public async Task Should_NotInvokeOnFail()
	{
		_validator
			.ValidateAsync(Arg.Is(_dummyCommand))
			.Returns(new ValidationResult([new ValidationFailure()]));

		await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await _validator.Received(1).ValidateAsync(Arg.Is(_dummyCommand));
		await _nextDelegate.DidNotReceive().Invoke();
	}

	[Fact]
	public async Task Should_InvokeOnSuccess()
	{
		_validator
			.ValidateAsync(Arg.Is(_dummyCommand))
			.Returns(new ValidationResult());

		await _behavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		await _validator.Received(1).ValidateAsync(Arg.Is(_dummyCommand));
		await _nextDelegate.Received(1).Invoke();
	}
}