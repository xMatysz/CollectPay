using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Queries;

public class WhenSendingGetBillsQuery : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly GetBillsQueryHandler _handler;

	public WhenSendingGetBillsQuery(WebApiFactory factory)
		: base(factory)
	{
		_handler = new GetBillsQueryHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldReturnBillsFromDb()
	{
		var bills = new List<Bill>
		{
			new BillBuilder().Build(),
			new BillBuilder().Build(),
			new BillBuilder().Build()
		};

		foreach (var bill in bills)
		{
			await BillRepository.AddAsync(bill);
		}

		await UnitOfWork.SaveChangesAsync();

		var result = await _handler.Handle(new GetBillsQuery(), CancellationToken.None);

		result.Should().BeEquivalentTo(bills);
	}
}