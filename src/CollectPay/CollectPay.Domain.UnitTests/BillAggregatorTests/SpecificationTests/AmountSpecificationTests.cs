using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class AmountSpecificationTests
{
	[Fact]
	public void ShouldCreateCorrectAmount()
	{
		const decimal expectedValue = 12.42m;
		const string expectedCurrency = "EUR";

		var amount = Amount.Create(expectedValue, expectedCurrency);

		amount.IsError.Should().BeFalse();
		amount.Value.Value.Should().Be(expectedValue);
		amount.Value.Currency.Should().Be(expectedCurrency);
	}
	[Fact]
	public void ShouldThrowWhenCurrencyIsLongerThen3Letters()
	{
		const string incorrectCurrency = "TEST";

		var amount = Amount.Create(21.37m, incorrectCurrency);

		amount.IsError.Should().BeTrue();
		amount.FirstError.Should().BeEquivalentTo(AmountErrors.IncorrectCurrency);
	}

	[Fact]
	public void ShouldThrowWhenValueIsNegative()
	{
		const decimal incorrectValue = -1m;

		var amount = Amount.Create(incorrectValue, "USD");

		amount.IsError.Should().BeTrue();
		amount.FirstError.Should().BeEquivalentTo(AmountErrors.IncorrectValue);
	}
}