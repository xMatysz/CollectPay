using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands.Payment;

public class SendPaymentCommandBase :IntegrationTestBase
{
	protected readonly Bill Bill;

	protected SendPaymentCommandBase(WebApiFactory factory)
		: base(factory)
	{
		Bill = BillBuilder.Build();
		AssumeEntityInDb(Bill);
	}
}