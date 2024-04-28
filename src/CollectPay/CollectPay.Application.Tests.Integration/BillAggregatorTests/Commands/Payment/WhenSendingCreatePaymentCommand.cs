using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Payment;

public class WhenSendingCreatePaymentCommand : SendPaymentCommandBase
{
	public WhenSendingCreatePaymentCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldAddPaymentToBillAndToDb()
	{
		var command = new CreatePaymentCommand(
			Bill.Id,
			Guid.NewGuid(),
			true,
			Amount.Create(21.37m, "USD").Value,
			[
				Guid.NewGuid(),
				Guid.NewGuid()
			]);

		await Sender.Send(command);

		var bill = await BillRepository.GetByIdAsync(Bill.Id);
		bill!.Payments.Should().HaveCount(1);
	}
}