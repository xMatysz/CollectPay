namespace CollectionPay.Contracts.Requests.User;

public record RegisterUserRequest(
	string Email,
	string Password);