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

	private Amount()
	{
	}
}

public static class AmountErrors
{
	public static Error IncorrectCurrency => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(IncorrectCurrency)}",
		description: "Currency can't have more then 3 letters");

	public static Error IncorrectValue => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(IncorrectValue)}",
		description: "Value cannot be lower then 0");
}