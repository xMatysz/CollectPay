using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.RemoveBillTests;

public class WhenHandlingRemoveBillCommand : UnitTestBase
{
	private readonly RemoveBillCommandHandler _handler;

	public WhenHandlingRemoveBillCommand()
	{
		 _handler = new RemoveBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())!
			.ReturnsForAnyArgs(Task.FromResult<Domain.BillAggregate.Bill>(null!));

		var command = new RemoveBillCommand(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenUserIsNotAttachedToBill()
	{
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(BillBuilder.Default().Build());

		var command = new RemoveBillCommand(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenNotCreatorTryRemove()
	{
		var user = Guid.NewGuid();
		var bill = BillBuilder.Default().Build();
		bill.Update(BillBuilder.DefaultName, [user], []);
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(bill);

		var command = new RemoveBillCommand(user, Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.OnlyCreatorCanRemoveBill);
	}
}