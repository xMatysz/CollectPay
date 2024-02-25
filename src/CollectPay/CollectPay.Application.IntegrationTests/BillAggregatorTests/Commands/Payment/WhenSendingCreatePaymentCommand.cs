using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Domain.BillAggregate.ValueObjects;
using ErrorOr;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands.Payment;

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

		var result = await Sender.Send(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Created);

	}
}