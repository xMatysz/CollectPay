using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Queries.Bills.GetBillsTests;

public class WhenHandlingGetBillsQuery : UnitTestBase
{
	private readonly GetBillsQueryHandler _handler;

	public WhenHandlingGetBillsQuery()
	{
		_handler = new GetBillsQueryHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Return_BillsCreatedOrAssignedToUser()
	{
	}
}