using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

namespace CollectPay.Application.UnitTests.BillAggregateTests.Bills;

public class WhenValidatingCreateBillCommand
{
	private readonly CreateBillCommandValidator _validator = new();

	[Fact]
	public async Task ShouldFailWhenUserIdIsEmpty()
	{
		var emptyUserId = Guid.Empty;
		var command = new CreateBillCommand(emptyUserId, "Test");

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
	}
}