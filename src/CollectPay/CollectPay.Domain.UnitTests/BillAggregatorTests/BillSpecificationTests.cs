using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Tests.Shared.Builders;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests;

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

	[Fact]
	public void ShouldCreatePayment()
	{
		var creatorId = Guid.NewGuid();
		var amount = new Amount(decimal.One, "PLN");
		var debtors = new[] { Guid.NewGuid(), Guid.NewGuid() };

		var payment = new PaymentBuilder()
			.WithCreatorId(creatorId)
			.WithAmount(amount)
			.WithDebtors(debtors)
			.WithCreatorIncluded()
			.Build();

		payment.CreatorId.Should().Be(creatorId);
		payment.Amount.Should().BeEquivalentTo(amount);
		payment.DebtorIds.Should().BeEquivalentTo(debtors);
		payment.IsCreatorIncluded.Should().BeTrue();
	}
}