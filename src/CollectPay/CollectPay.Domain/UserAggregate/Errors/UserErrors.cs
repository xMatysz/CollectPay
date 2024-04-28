using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Domain.UserAggregate.Errors;

public static class UserErrors
{
	public static Error InvalidEmail => Error.Validation(
		code: $"{nameof(BillErrors)}.{nameof(InvalidEmail)}",
		description: "Email is invalid");
}