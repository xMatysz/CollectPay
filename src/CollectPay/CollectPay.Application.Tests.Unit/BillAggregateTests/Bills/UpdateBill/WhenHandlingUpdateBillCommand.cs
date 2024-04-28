using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Bills.UpdateBill;

public class WhenHandlingUpdateBillCommand : UnitTestBase
{
	private readonly UpdateBillCommandHandler _handler;

	public WhenHandlingUpdateBillCommand()
	{
		_handler = new UpdateBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldFailWhenBillIsNotFound()
	{
		var command = new UpdateBillCommand(Guid.NewGuid(), null!);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task ShouldSuccessFullyFinishHandling()
	{
		var bill = new BillBuilder().Build();
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var command = new UpdateBillCommand(bill.Id, new UpdateBillInfo("NewName"));

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().BeEquivalentTo(Result.Updated);
	}
}