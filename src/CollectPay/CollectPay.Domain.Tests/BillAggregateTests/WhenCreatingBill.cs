using CollectPay.Domain.BillAggregate;

namespace CollectPay.Domain.Tests.BillAggregateTests;

public class WhenCreatingBill
{
	[Fact]
	public void ShouldCreateBillWithEmptyPaymentsAndDebts()
	{
		var bill = new Bill(Guid.NewGuid(), "testName", new List<Guid>());

		bill.Payments.Should().BeEmpty();
		bill.Debts.Should().BeEmpty();
	}
}