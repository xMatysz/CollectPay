using CollectPay.Domain.BillAggregate;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Payment;

public class SendPaymentCommandBase : IntegrationTestBase
{
	protected readonly Bill Bill;

	protected SendPaymentCommandBase(WebApiFactory factory)
		: base(factory)
	{
		Bill = new BillBuilder().Build();
		AssumeEntityInDb(Bill);
	}
}