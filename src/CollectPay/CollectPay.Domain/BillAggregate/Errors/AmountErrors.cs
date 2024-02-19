using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class AmountErrors
{
	public static Error IncorrectCurrency => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(IncorrectCurrency)}",
		description: "Currency can't have more then 3 letters");

	public static Error IncorrectValue => Error.Validation(
		code: $"{nameof(AmountErrors)}.{nameof(IncorrectValue)}",
		description: "Value cannot be lower then 0");
}