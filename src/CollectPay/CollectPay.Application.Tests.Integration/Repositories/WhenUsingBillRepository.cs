using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.Repositories;

public class WhenUsingBillRepository : IntegrationTestBase
{
	public WhenUsingBillRepository(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task GetAllForUserAsync_Should_Return_BillsCreatedOrAttachedToUser()
	{
		var userId = Guid.NewGuid();
		// assigned to user
		var createdBill = BillBuilder.Default().WithCreatorId(userId).Build();
		var assignedBill = BillBuilder.Default().BuildWithDebtors([userId]);

		// other bills
		var customBill = BillBuilder.Default().Build();
		var customBill2 = BillBuilder.Default().Build();
		await AssumeEntityInDbAsync(createdBill, assignedBill, customBill, customBill2);

		var result = await BillRepository.GetAllForUserAsync(userId);

		result.Should().HaveCount(2);
		result.Where(x=>x.CreatorId == userId).Should().HaveCount(1);
		result.Where(x => x.Debtors.Contains(userId)).Should().HaveCount(2);
	}
}