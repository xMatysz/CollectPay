using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Bills.RemoveBill;

public class WhenHandlingRemoveBillCommand : UnitTestBase
{
	private readonly RemoveBillCommandHandler _handler;

	public WhenHandlingRemoveBillCommand()
	{
		_handler = new RemoveBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldFailWhenBillIsNotFound()
	{
		var command = new RemoveBillCommand(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_When_CreatorIdNotMatch()
	{
		var userId = Guid.NewGuid();
		var differentUserId = Guid.NewGuid();
		var bill = new BillBuilder().WithCreatorId(userId).Build();
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var command = new RemoveBillCommand(differentUserId, bill.Id);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.CannotBeRemovedByNotOwner);
	}

	[Fact]
	public async Task ShouldSuccessFullyFinishHandling()
	{
		var userId = Guid.NewGuid();
		var bill = new BillBuilder().WithCreatorId(userId).Build();
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var command = new RemoveBillCommand(userId, bill.Id);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().BeEquivalentTo(Result.Deleted);
		await BillRepository.Received(1).GetByIdAsync(Arg.Is(bill.Id), CancellationToken.None);
	}
}