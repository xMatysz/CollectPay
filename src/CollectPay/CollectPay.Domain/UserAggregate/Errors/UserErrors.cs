using ErrorOr;

namespace CollectPay.Domain.UserAggregate.Errors;

public static class UserErrors
{
	public static Error InvalidEmail => Error.Validation(
		code: $"{nameof(UserErrors)}.{nameof(InvalidEmail)}",
		description: "Email is invalid");

	public static Error UserAlreadyExist => Error.Validation(
		code: $"{nameof(UserErrors)}.{nameof(UserAlreadyExist)}",
		description: "User with specified email already exist");
}