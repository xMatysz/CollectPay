using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.RemovePaymentTests;

public class WhenValidatingRemovePaymentCommandInput
{
	private readonly RemovePaymentCommandInputValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdIsEmpty()
	{
		var command = new RemovePaymentCommand(Guid.Empty, Guid.NewGuid(), Guid.NewGuid());

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.UserId)));
	}

	[Fact]
	public async Task Should_Fail_WhenBillIdIsEmpty()
	{
		var command = new RemovePaymentCommand(Guid.NewGuid(), Guid.Empty, Guid.NewGuid());

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.BillId)));
	}


	[Fact]
	public async Task Should_Fail_WhenPaymentIdIsEmpty()
	{
		var command = new RemovePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.Empty);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(RemovePaymentCommand.PaymentId)));
	}
}