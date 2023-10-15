using CollectPay.Domain.Tests.TestsUtilities;

namespace CollectPay.Domain.Tests.BillAggregateTests;

public class WhenCreatingBill
{
	[Fact]
	public void ShouldCreateBillWithEmptyPaymentsAndDebts()
	{
		var creatorId = Guid.NewGuid();
		const string billName = "TestBill";

		var bill = new BillBuilder().WithCreatorId(creatorId).WithName(billName).Build();

		bill.CreatorId.Should().Be(creatorId);
		bill.Name.Should().Be(billName);
		bill.Payments.Should().BeEmpty();
		bill.Debts.Should().BeEmpty();
	}
}