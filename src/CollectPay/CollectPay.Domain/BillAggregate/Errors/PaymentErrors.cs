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
}
