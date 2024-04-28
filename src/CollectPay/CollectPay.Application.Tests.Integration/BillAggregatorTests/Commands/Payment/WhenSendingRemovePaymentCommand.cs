using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Payment;

public class WhenSendingRemovePaymentCommand : SendPaymentCommandBase
{
	public WhenSendingRemovePaymentCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldRemovePayment()
	{
		var payment = new PaymentBuilder().Build();
		Bill.AddPayment(payment);
		await UnitOfWork.SaveChangesAsync();

		var command = new RemovePaymentCommand(Bill.Id, payment.Id);

		await Sender.Send(command);

		var bill = await BillRepository.GetByIdAsync(Bill.Id);
		bill!.Payments.Should().BeEmpty();
	}
}