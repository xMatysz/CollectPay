using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.RemoveBillTests;

public class WhenValidatingRemoveBillCommand
{
	private readonly RemoveBillCommandValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdIsEmpty()
	{
		var command = new RemoveBillCommand(Guid.Empty, Guid.NewGuid());

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(RemoveBillCommand.UserId)));
	}


	[Fact]
	public async Task Should_Fail_WhenBillIdIsEmpty()
	{
		var command = new RemoveBillCommand(Guid.NewGuid(), Guid.Empty);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(RemoveBillCommand.BillId)));
	}
}