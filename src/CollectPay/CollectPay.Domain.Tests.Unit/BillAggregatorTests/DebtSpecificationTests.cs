using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.Tests.Unit.BillAggregatorTests;

public class DebtSpecificationTests
{
	[Fact]
	public void Create_Should_RoundAmountValueTo2Position()
	{
		const decimal debtAmount = 21.213769m;
		var roundedAmount = Math.Round(debtAmount, 2);

		var result = Debt.Create(Guid.NewGuid(), debtAmount, Guid.NewGuid());

		result.DebtAmount.Should().Be(roundedAmount);
	}
}