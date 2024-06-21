using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.CreateBillTests;

public class WhenValidatingCreateBillCommand
{
	private readonly CreateBillCommandValidator _validator = new();

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public async Task Should_Fail_WhenNameIsEmpty(string? billName)
	{
		var command = new CreateBillCommand(Guid.NewGuid(), billName);

		var result = await _validator.ValidateAsync(command, CancellationToken.None);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreateBillCommand.BillName)));
	}

	[Fact]
	public async Task Should_Fail_WhenCreatorIdIsEmpty()
	{
		var command = new CreateBillCommand(Guid.Empty, "TestName");

		var result = await _validator.ValidateAsync(command, CancellationToken.None);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreateBillCommand.CreatorId)));
	}
}