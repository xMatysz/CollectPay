using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Tests.Shared.Builders;

public class AmountBuilder : TestBuilder<Amount>
{
	private decimal _amount = DefaultAmountValue;
	private string _currency = DefaultCurrency;
	public static decimal DefaultAmountValue => 21.37m;
	public static string DefaultCurrency => "PLN";

	public static  AmountBuilder Default()
	{
		return new AmountBuilder();
	}

	public AmountBuilder WithAmount(decimal amount)
	{
		_amount = amount;
		return this;
	}

	public AmountBuilder WithCurrency(string currency)
	{
		_currency = currency;
		return this;
	}

	public override Amount Build()
	{
		return Amount.Create(_amount, _currency).Value;
	}
}