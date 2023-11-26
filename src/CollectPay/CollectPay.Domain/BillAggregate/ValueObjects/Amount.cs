using CollectPay.Domain.Common.Models;

namespace CollectPay.Domain.BillAggregate.ValueObjects;

public class Amount : ValueObject
{
	public decimal Value { get; }
	public string Currency { get; }

	public Amount(decimal amount, string currency)
	{
		Value = amount;
		Currency = currency;
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
		yield return Currency;
	}

	public Amount()
	{
	}
}