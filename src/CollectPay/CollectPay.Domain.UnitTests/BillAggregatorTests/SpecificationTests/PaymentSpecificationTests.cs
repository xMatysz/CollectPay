using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class PaymentSpecificationTests
{
	[Fact]
	public void ShouldCreatePayment()
	{
		var creatorId = Guid.NewGuid();
		var amount = Amount.Create(decimal.One, "PLN").Value;
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