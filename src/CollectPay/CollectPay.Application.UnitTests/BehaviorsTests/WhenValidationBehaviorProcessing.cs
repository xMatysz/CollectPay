using CollectPay.Application.Behaviors;
using CollectPay.Application.UnitTests.Utilities.TestDoubles;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;

namespace CollectPay.Application.UnitTests.BehaviorsTests;

public class WhenValidationBehaviorProcessing
{
	private readonly DummyCommand _dummyCommand = new();
	private readonly RequestHandlerDelegate<ErrorOr<IErrorOr>> _nextDelegate = Substitute.For<RequestHandlerDelegate<ErrorOr<IErrorOr>>>();

	[Fact]
	public async Task ShouldPassIfCommandNotHaveValidator()
	{
		var validationBehavior = new ValidationBehavior<DummyCommand, ErrorOr<IErrorOr>>(Enumerable.Empty<IValidator<DummyCommand>>());

		var result = await validationBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		result.IsError.Should().BeFalse();
	}

	[Fact]
	public async Task ShouldPassIfSatisfyValidator()
	{
		var successValidator = new SuccessDummyCommandValidator();
		var validationBehavior = new ValidationBehavior<DummyCommand, ErrorOr<IErrorOr>>(new[] { successValidator });

		var result = await validationBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		result.IsError.Should().BeFalse();
	}

	[Fact]
	public async Task ShouldFailIfNotSatisfyValidator()
	{
		var failValidator = new FailDummyCommandValidator();
		var validationBehavior = new ValidationBehavior<DummyCommand, ErrorOr<IErrorOr>>(new[] { failValidator });

		var result = await validationBehavior.Handle(_dummyCommand, _nextDelegate, CancellationToken.None);

		result.IsError.Should().BeTrue();
	}
}