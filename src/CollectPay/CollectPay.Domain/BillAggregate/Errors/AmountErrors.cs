using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class AmountErrors
{
	public static Error AmountValueHaveToBePositive => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(AmountValueHaveToBePositive)}",
		description: "Amount value have to greater then 0");

	public static Error CurrencyHaveToHave3CharactersLength => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(CurrencyHaveToHave3CharactersLength)}",
		description: "Amount currency have to have 3 characters");
}