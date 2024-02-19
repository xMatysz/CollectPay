using ErrorOr;

namespace CollectPay.Domain.BillAggregate.Errors;

public static class BillErrors
{
	public static Error BillNotFound => Error.NotFound(
		code: $"{nameof(BillErrors)}.{nameof(BillNotFound)}",
		description: "Bill was not found");
}