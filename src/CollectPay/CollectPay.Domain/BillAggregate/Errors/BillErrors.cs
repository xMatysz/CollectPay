using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class BillErrors
{
	public static Error BillNotFound => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(BillNotFound)}",
		description: "Bill was not found");

	public static Error OnlyCreatorCanRemoveBill => Error.Forbidden(
		code: $"{nameof(BillErrors)}.{nameof(OnlyCreatorCanRemoveBill)}",
		description: "Only creator can remove bill");

	// After Tests
	public static Error PaymentAlreadyExist => Error.Conflict(
		code: $"{nameof(BillErrors)}.{nameof(PaymentAlreadyExist)}",
		description: "Payment is already added to bill");

	public static Error PaymentNotExist => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(PaymentNotExist)}",
		description: "Payment not exist in bill");

	public static Error NameCannotBeEmpty => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(NameCannotBeEmpty)}",
		description: "Bill name cannot be empty");

	public static Error CannotRemoveCreatorFromDebtors => Error.Conflict(
		code: $"{nameof(BillErrors)}.{nameof(CannotRemoveCreatorFromDebtors)}",
		description: "Cannot remove Creator from bill Debtors");

	public static Error DebtorIsAlreadyAdded(Guid  userId) => Error.Conflict(
		code: $"{nameof(BillErrors)}.{nameof(DebtorIsAlreadyAdded)}",
		description: $"User with id {userId} is already added as a debtor");

	public static Error DebtorNotFound(Guid userId) => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(DebtorNotFound)}",
		description: $"User with {userId} id is not on Debtors list");
}