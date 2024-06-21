using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.RemovePaymentTests;

public class WhenHandlingRemovePaymentCommand : UnitTestBase
{
	private readonly RemovePaymentCommandHandler _handler;

	public WhenHandlingRemovePaymentCommand()
	{
		_handler = new RemovePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var command = new RemovePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenUserIsNotAttached()
	{
		BillRepository.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(BillBuilder.Default().Build());

		var command = new RemovePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenPaymentNotExist()
	{
		var userId = Guid.NewGuid();
		var bill = BillBuilder.Default().Build();
		bill.Update(bill.Name, [userId], []);
		BillRepository.GetByIdAsync(Arg.Is(bill.Id))
			.Returns(bill);

		var command = new RemovePaymentCommand(userId, bill.Id, Guid.NewGuid());

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.PaymentNotExist);
	}

	[Fact]
	public async Task Should_RemovePaymentFromBill()
	{
		var userId = Guid.NewGuid();
		var payment = PaymentBuilder.Default().Build();
		var bill = AssumeBillWithAttachedUser(userId);
		bill.AddPayment(payment);
		BillRepository
			.GetByIdAsync(Arg.Is(bill.Id))
			.Returns(bill);

		var command = new RemovePaymentCommand(userId, bill.Id, payment.Id);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Deleted);
		bill.Payments.Should().BeEmpty();
	}

	private static Bill AssumeBillWithAttachedUser(Guid userId)
	{
		var bill = BillBuilder.Default().Build();
		bill.Update(BillBuilder.DefaultName, [userId], []);
		return bill;
	}
}