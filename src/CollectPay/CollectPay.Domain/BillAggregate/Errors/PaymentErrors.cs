using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class PaymentErrors
{
	public static Error PaymentNotFound => Error.NotFound(
		code: $"{nameof(PaymentErrors)}.{nameof(PaymentNotFound)}",
		description: "Payment was not found");

	// after tests
	public static Error NameCannotBeEmpty => Error.NotFound(
		code: $"{nameof(PaymentErrors)}.{nameof(NameCannotBeEmpty)}",
		description: "Payment name cannot be empty");

	public static Error InvalidAmount => Error.NotFound(
		code: $"{nameof(PaymentErrors)}.{nameof(InvalidAmount)}",
		description: "Amount is invalid");

	public static Error UserIsAlreadyAdded(Guid  userId) => Error.Conflict(
		code: $"{nameof(PaymentErrors)}.{nameof(UserIsAlreadyAdded)}",
		description: $"User with id {userId} is already added to payment");

	public static Error UserNotFound(Guid userId) => Error.NotFound(
		code: $"{nameof(PaymentErrors)}.{nameof(UserNotFound)}",
		description: $"User with {userId} id not is not assigned to payment");
}
