using CollectPay.Application.Common.Interactions;

namespace CollectPay.Application.UserAggregate.Commands.CreateUser;

public record CreateUserCommand(string Email, string Password, string NickName) : ICommand<ErrorOr<Created>>;