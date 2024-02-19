namespace CollectionPay.Contracts.Requests;

public record CreateUserRequest(string Email, string Password, string NickName);