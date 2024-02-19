using CollectPay.Application.BillAggregate.Queries.GetPayments;
using CollectPay.Domain.BillAggregate.Errors;

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

	[Fact]
	public async Task ShouldReturnErrorWhenBillNotExist()
	{
		var fakeBillId = Guid.NewGuid();

		var query = new GetPaymentsQuery(fakeBillId);

		var result = await _handler.Handle(query, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(BillErrors.BillNotFound);
	}
}