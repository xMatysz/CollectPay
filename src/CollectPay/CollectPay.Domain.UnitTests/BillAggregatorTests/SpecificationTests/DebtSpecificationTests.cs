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

		var debt = new Debt(expectedDebtor, debtAmount, expectedCreditor);

		debt.Debtor.Should().Be(expectedDebtor);
		debt.DebtAmount.Should().Be(debtAmount);
		debt.Creditor.Should().Be(expectedCreditor);
	}
}