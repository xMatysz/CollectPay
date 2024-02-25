using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class PaymentErrors
{
	public static Error PaymentNotFound => Error.NotFound(
		code: $"{nameof(PaymentErrors)}.{nameof(PaymentNotFound)}",
		description: "Payment was not found");

	public static Error PaymentAlreadyExist => Error.Conflict(
		code: $"{nameof(PaymentErrors)}.{nameof(PaymentAlreadyExist)}",
		description: "Payment already exist");

	public static Error CreatorCannotBeDebtor => Error.Conflict(
		code: $"{nameof(PaymentErrors)}.{nameof(CreatorCannotBeDebtor)}",
		description: "Creator cannot be on Debtors list, please use 'Include Creator' option");

	public static Error InvalidPayment => Error.Conflict(
		code: $"{nameof(PaymentErrors)}.{nameof(InvalidPayment)}",
		description: "Invalid payment");
}
