using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class BillErrors
{
	public static Error BillNotFound => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(BillNotFound)}",
		description: "Bill was not found");

	public static Error CannotBeRemovedByNotOwner => Error.Forbidden(
		code: $"{nameof(BillErrors)}.{nameof(CannotBeRemovedByNotOwner)}",
		description: "Only creator can remove bill");

	public static Error CreatorCannotBeDebtor => Error.Validation(
		code: $"{nameof(BillErrors)}.{nameof(CreatorCannotBeDebtor)}",
		description: "Creator cannot be a debtor");

	public static Error DebtorIsAlreadyAdded(Guid  userId) => Error.Conflict(
		code: $"{nameof(BillErrors)}.{nameof(DebtorIsAlreadyAdded)}",
		description: $"User with id {userId} is already added as a debtor");

	public static Error DebtorNotFound(Guid userId) => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(DebtorNotFound)}",
		description: $"User with {userId} id is not on Debtors list");
}