using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.CreateBillTests;

public class WhenHandlingCreateBillCommand : UnitTestBase
{
	private readonly CreateBillCommandHandler _handler;

	public WhenHandlingCreateBillCommand()
	{
		_handler = new CreateBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task Should_AddBillToRepository()
	{
		var command = new CreateBillCommand(Guid.NewGuid(), "TestName");

		await _handler.Handle(command, CancellationToken.None);

		await BillRepository.Received(1).AddAsync(Arg.Is<Domain.BillAggregate.Bill>(b =>
			b.CreatorId == command.CreatorId &&
			b.Name == command.BillName));
	}
}