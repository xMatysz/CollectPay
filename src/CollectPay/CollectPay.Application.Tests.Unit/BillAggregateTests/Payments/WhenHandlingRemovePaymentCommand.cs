using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Payments;

public class WhenHandlingRemovePaymentCommand : UnitTestBase
{
	private readonly RemovePaymentCommandHandler _handler;

	public WhenHandlingRemovePaymentCommand()
	{
		_handler = new RemovePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldFailWhenBillNotExist()
	{
		var command = new RemovePaymentCommand(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task ShouldFailWhenPaymentNotExist()
	{
		var bill = new BillBuilder().Build();
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var command = new RemovePaymentCommand(bill.Id, Guid.NewGuid());

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.PaymentNotFound);
	}

	[Fact]
	public async Task ShouldRemovePayment()
	{
		var bill = new BillBuilder().Build();
		var payment = new PaymentBuilder().WithBillId(bill.Id).Build();
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var command = new RemovePaymentCommand(bill.Id, payment.Id);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Deleted);
	}
}