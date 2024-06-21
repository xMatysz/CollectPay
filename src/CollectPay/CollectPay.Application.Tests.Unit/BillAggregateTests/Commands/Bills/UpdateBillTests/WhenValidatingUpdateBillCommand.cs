using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.UpdateBillTests;

public class WhenValidatingUpdateBillCommand
{
	private readonly UpdateBillCommandValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdIsEmpty()
	{
		var command = new UpdateBillCommand(Guid.NewGuid(), Guid.Empty, new UpdateBillInfo("Name", [], []));

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.UserId)));
	}


	[Fact]
	public async Task Should_Fail_WhenBillIsEmpty()
	{
		var command = new UpdateBillCommand(Guid.Empty, Guid.NewGuid(), new UpdateBillInfo("Name", [], []));

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.BillId)));
	}


	[Fact]
	public async Task Should_Fail_WhenUpdateInfoIsNull()
	{
		var command = new UpdateBillCommand(Guid.NewGuid(), Guid.NewGuid(), null);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(UpdateBillCommand.UpdateBillInfo)));
	}
}