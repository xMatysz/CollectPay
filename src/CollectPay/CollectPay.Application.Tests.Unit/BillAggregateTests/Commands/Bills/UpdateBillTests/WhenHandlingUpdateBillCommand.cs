using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.UserAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Bills.UpdateBillTests;

public class WhenHandlingUpdateBillCommand : UnitTestBase
{
	private readonly UpdateBillCommandHandler _handler;

	public WhenHandlingUpdateBillCommand()
	{
		_handler = new UpdateBillCommandHandler(BillRepository, UserRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var command = new UpdateBillCommand(
			Guid.NewGuid(),
			Guid.NewGuid(),
			new UpdateBillInfo("Name", [], []));

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}


	[Fact]
	public async Task Should_Fail_WhenUpdateInfoContainsNotExistingUser()
	{
		var requestingUser = Guid.NewGuid();
		var bill = BillBuilder.Default().WithCreatorId(requestingUser).Build();

		const string notExistingUser = "missing@user.com";
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(bill);

		var command = new UpdateBillCommand(
			Guid.NewGuid(),
			requestingUser,
			new UpdateBillInfo("Name", [notExistingUser], [notExistingUser]));

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(UserErrors.UserNotFound(notExistingUser));
	}

	[Fact]
	public async Task Should_Fail_WhenRequestingUserIsNotAttachedToBill()
	{
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(BillBuilder.Default().Build());
		var command = new UpdateBillCommand(
			Guid.NewGuid(),
			Guid.NewGuid(),
			new UpdateBillInfo("Name", [], []));

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}
}