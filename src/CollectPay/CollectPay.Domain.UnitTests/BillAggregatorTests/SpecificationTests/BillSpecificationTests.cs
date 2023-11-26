using CollectPay.Domain.BillAggregate;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class BillSpecificationTests
{
	private readonly Bill _bill = new BillBuilder().Build();

	[Fact]
	public void ShouldAddPayment()
	{
		var payment = new PaymentBuilder().Build();

		_bill.AddPayment(payment);

		_bill.Payments.Should().HaveCount(1);
		_bill.Payments.Should().BeEquivalentTo(new[] { payment });
	}

	[Fact]
	public void ShouldDeletePayment()
	{
		var payment = new PaymentBuilder().Build();
		_bill.AddPayment(payment);

		_bill.DeletePayment(payment.Id);

		_bill.Payments.Should().BeEmpty();
	}

	[Fact]
	public void ShouldCreateBill()
	{
		const string billName = "TestName";
		var creatorId = Guid.NewGuid();

		var bill = new BillBuilder()
			.WithName(billName)
			.WithCreatorId(creatorId)
			.Build();

		bill.Name.Should().Be(billName);
		bill.CreatorId.Should().Be(creatorId);
	}
}