using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.UpdatePaymentTests;

public class WhenValidatingUpdatePaymentCommand
{
	private readonly UpdatePaymentCommandInputValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdIsEmpty()
	{
		var command = new UpdatePaymentCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid(),
			new UpdatePaymentInfo(null, null, [], []));

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.UserId)));
	}


	[Fact]
	public async Task Should_Fail_WhenBillIdIsEmpty()
	{
		var command = new UpdatePaymentCommand(Guid.NewGuid(), Guid.Empty, Guid.NewGuid(),
			new UpdatePaymentInfo(null, null, [], []));

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.BillId)));
	}

	[Fact]
	public async Task Should_Fail_WhenPaymentIdIsEmpty()
	{
		var command = new UpdatePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty,
			new UpdatePaymentInfo(null, null, [], []));

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should().Be(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.PaymentId)));
	}

	[Fact]
	public async Task Should_Fail_WhenUpdateInfoIdIsNull()
	{
		var command = new UpdatePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdatePaymentCommand.UpdatePaymentInfo)));
	}
}