using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class PaymentSpecificationTests
{
	[Fact]
	public void ShouldCreatePayment()
	{
		var billId = Guid.NewGuid();
		var creatorId = Guid.NewGuid();
		var amount = Amount.Create(decimal.One, "PLN").Value;
		var debtors = new[] { Guid.NewGuid(), Guid.NewGuid() };

		var payment = new PaymentBuilder()
			.WithBillId(billId)
			.WithCreatorId(creatorId)
			.WithAmount(amount)
			.WithDebtors(debtors)
			.WithCreatorIncluded()
			.Build();

		payment.BillId.Should().Be(billId);
		payment.CreatorId.Should().Be(creatorId);
		payment.Amount.Should().BeEquivalentTo(amount);
		payment.DebtorIds.Should().BeEquivalentTo(debtors);
		payment.IsCreatorIncluded.Should().BeTrue();
	}

	[Fact]
	public void ShouldNotCreateWhenCreatorIsInDebtorsList()
	{
		var user1 = Guid.NewGuid();
		var user2 = Guid.NewGuid();

		var payment = Payment
			.Create(Guid.NewGuid(),
				user1,
				false,
				null,
				new[] { user1, user2 });

		payment.IsError.Should().BeTrue();
		payment.FirstError.Should().BeEquivalentTo(PaymentErrors.CreatorCannotBeDebtor);
	}
}