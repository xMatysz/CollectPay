using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.Common.Models;
using ErrorOr;

namespace CollectPay.Domain.BillAggregate.ValueObjects;

public class Amount : ValueObject
{
	private const int _maxLengthOfCurrency = 3;
	public decimal Value { get; }
	public string Currency { get; }

	private Amount(decimal amount, string currency)
	{
		Value = amount;
		Currency = currency;
	}

	public static ErrorOr<Amount> Create(decimal amount, string currency)
	{
		if (amount < 0m)
		{
			return AmountErrors.IncorrectValue;
		}

		if (currency.Length > _maxLengthOfCurrency)
		{
			return AmountErrors.IncorrectCurrency;
		}

		return new Amount(amount, currency);
	}

	protected override IEnumerable<object> GetEqualityComponents()
	{
		yield return Value;
		yield return Currency;
	}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	private Amount()
	{
	}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}