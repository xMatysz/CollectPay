using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class DebtSpecificationTests
{
	[Fact]
	public void ShouldCreateCorrectDebt()
	{
		var expectedDebtor = Guid.NewGuid();
		const decimal debtAmount = 12m;
		var expectedCreditor = Guid.NewGuid();

		var debt = Debt.Create(expectedDebtor, debtAmount, expectedCreditor);

		debt.Debtor.Should().Be(expectedDebtor);
		debt.DebtAmount.Should().Be(debtAmount);
		debt.Creditor.Should().Be(expectedCreditor);
	}

	[Fact]
	public void ShouldRoundAmount()
	{
		const decimal expectedAmount = 3.33m;

		var debt = Debt.Create(Guid.NewGuid(), 3.3333333333m, Guid.NewGuid());

		debt.DebtAmount.Should().Be(expectedAmount);
	}
}