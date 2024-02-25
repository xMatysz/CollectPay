using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Domain.BillAggregate;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Queries;

public class WhenSendingGetBillsQuery : IntegrationTestBase
{
	public WhenSendingGetBillsQuery(WebApiFactory factory)
		: base(factory)
	{
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

		var result = await Sender.Send(new GetBillsQuery());

		result.Value.Should().BeEquivalentTo(bills);
	}
}