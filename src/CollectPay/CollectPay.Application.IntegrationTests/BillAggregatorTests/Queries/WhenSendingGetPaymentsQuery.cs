using CollectPay.Application.BillAggregate.Queries.GetPayments;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Queries;

public class WhenSendingGetPaymentsQuery : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly GetPaymentsQueryHandler _handler;

	public WhenSendingGetPaymentsQuery(WebApiFactory factory)
		: base(factory)
	{
		_handler = new GetPaymentsQueryHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldReturnPaymentsFromDb()
	{
		var bill = BillBuilder.Build();

		await AssumeEntityInDb(bill);

		var payments = new[]
		{
			PaymentBuilder.WithBillId(bill.Id).Build(),
			PaymentBuilder.WithBillId(bill.Id).Build(),
			PaymentBuilder.WithBillId(bill.Id).Build(),
		};

		await AssumeEntityInDb(payments);

		var query = new GetPaymentsQuery(bill.Id);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.Value.Should().BeEquivalentTo(payments);
	}
}