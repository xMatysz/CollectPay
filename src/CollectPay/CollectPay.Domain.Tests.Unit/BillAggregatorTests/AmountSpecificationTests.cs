using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Domain.Tests.Unit.BillAggregatorTests;

public class AmountSpecificationTests
{
	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	public void Create_Fail_WhenAmountValueIsNotPositive(decimal amountValue)
	{
		var result = Amount.Create(amountValue, AmountBuilder.DefaultCurrency);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(AmountErrors.AmountValueHaveToBePositive);
	}

	[Theory]
	[InlineData("PL")]
	[InlineData("USED")]
	public void Create_Fail_WhenAmountCurrencyLengthIsNot3(string currency)
	{
		var result = Amount.Create(AmountBuilder.DefaultAmountValue, currency);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(AmountErrors.CurrencyHaveToHave3CharactersLength);
	}
}