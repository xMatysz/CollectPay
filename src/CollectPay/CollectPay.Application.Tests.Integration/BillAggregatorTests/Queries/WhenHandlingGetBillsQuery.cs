using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Application.Dtos;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Queries;

public class WhenHandlingGetBillsQuery : IntegrationTestBase
{
	public WhenHandlingGetBillsQuery(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_Return_BillsAssignedToUser()
	{
		var userId = Guid.NewGuid();
		// assigned to user
		var createdBill = BillBuilder.Default().WithCreatorId(userId).Build();
		var assignedBill = BillBuilder.Default().BuildWithDebtors([userId]);

		// other bills
		var customBill = BillBuilder.Default().Build();
		var customBill2 = BillBuilder.Default().Build();
		await AssumeEntityInDbAsync(createdBill, assignedBill, customBill, customBill2);

		var query = new GetBillsQuery(userId);
		var result = await Sender.Send(query);

		result.IsError.Should().BeFalse();
		result.Value.Should().HaveCount(2);
	}
}