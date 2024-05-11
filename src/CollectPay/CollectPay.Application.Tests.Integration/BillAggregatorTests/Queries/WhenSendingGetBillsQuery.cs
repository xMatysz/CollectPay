using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Domain.BillAggregate;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Queries;

public class WhenSendingGetBillsQuery : IntegrationTestBase
{
	public WhenSendingGetBillsQuery(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldReturnUserBills()
	{
		var userId = Guid.NewGuid();

		var bills = new List<Bill>
		{
			new BillBuilder().WithCreatorId(userId).Build(),
			new BillBuilder().WithCreatorId(userId).Build(),
			new BillBuilder().Build()
		};

		foreach (var bill in bills)
		{
			await BillRepository.AddAsync(bill);
		}

		await UnitOfWork.SaveChangesAsync();

		var result = await Sender.Send(new GetBillsQuery(userId));

		result.Value.Should().HaveCount(2);
	}
}