using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.UpdatePaymentTests;

public class WhenHandlingUpdatePaymentCommand : UnitTestBase
{
	private readonly UpdatePaymentCommandHandler _handler;

	public WhenHandlingUpdatePaymentCommand()
	{
		_handler = new UpdatePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var command = new UpdatePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenUserIsNotAttachedToBill()
	{
		var bill = BillBuilder.Default().Build();
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var command = new UpdatePaymentCommand(Guid.NewGuid(), bill.Id, Guid.NewGuid(), null);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenPaymentNotExist()
	{
		var userId = Guid.NewGuid();
		var bill = BillBuilder.Default().BuildWithDebtors([userId]);
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var command = new UpdatePaymentCommand(userId, bill.Id, Guid.NewGuid(), null);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.PaymentNotExist);
	}

	[Fact]
	public async Task Should_UpdateName()
	{
		var userId = Guid.NewGuid();
		var payment = PaymentBuilder.Default().Build();
		var bill = BillBuilder.Default().BuildWithDebtors([userId]);
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		const string expectedName = "CorrectName";
		var command = new UpdatePaymentCommand(userId, bill.Id, payment.Id,
			new UpdatePaymentInfo(expectedName, AmountBuilder.Default().Build(), [], []));

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Updated);
		bill.Payments.Single().Name.Should().Be(expectedName);
	}

	[Fact]
	public async Task Should_UpdateAmount()
	{
		var userId = Guid.NewGuid();
		var payment = PaymentBuilder.Default().Build();
		var bill = BillBuilder.Default().BuildWithDebtors([userId]);
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(Arg.Is(bill.Id)).Returns(bill);
		var newAmount = AmountBuilder.Default().WithAmount(21.37m).WithCurrency("PLN").Build();
		var command = new UpdatePaymentCommand(userId, bill.Id, payment.Id,
			new UpdatePaymentInfo(PaymentBuilder.DefaultName, newAmount, [], []));

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Updated);
		bill.Payments.Single().Amount.Should().Be(newAmount);
	}
}