using CollectPay.Domain.Tests.TestsUtilities;

namespace CollectPay.Domain.Tests.BillAggregateTests;

public class WhenCreatingBill
{
	[Fact]
	public void ShouldCreateBillWithEmptyPaymentsAndDebts()
	{
		var billId = Guid.NewGuid();
		const string billName = "TestBill";

		var bill = new BillBuilder().WithCreatorId(billId).WithName(billName).Build();

		bill.CreatorId.Should().Be(billId);
		bill.Name.Should().Be(billName);
		bill.Payments.Should().BeEmpty();
		bill.Debts.Should().BeEmpty();
	}
}