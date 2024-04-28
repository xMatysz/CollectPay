using CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Queries;

public class WhenSendingGetPaymentsQuery : IntegrationTestBase
{
	public WhenSendingGetPaymentsQuery(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldReturnPaymentsFromDb()
	{
		var bill = new BillBuilder().Build();

		await AssumeEntityInDbAsync(bill);

		var payments = new[]
		{
			new PaymentBuilder().WithBillId(bill.Id).Build(),
			new PaymentBuilder().WithBillId(bill.Id).Build(),
			new PaymentBuilder().WithBillId(bill.Id).Build(),
		};

		await AssumeEntityInDbAsync(payments);

		var query = new GetPaymentsQuery(bill.Id);

		var result = await Sender.Send(query);

		result.Value.Should().BeEquivalentTo(payments);
	}

	[Fact]
	public async Task ShouldReturnErrorWhenBillNotExist()
	{
		var fakeBillId = Guid.NewGuid();

		var query = new GetPaymentsQuery(fakeBillId);

		var result = await Sender.Send(query);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(BillErrors.BillNotFound);
	}
}