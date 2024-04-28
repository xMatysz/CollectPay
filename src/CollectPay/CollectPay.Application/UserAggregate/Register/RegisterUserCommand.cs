using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.UserAggregate;
using ErrorOr;

namespace CollectPay.Application.UserAggregate.Register;

public sealed record RegisterUserCommand(
	string Email,
	string Password) : ICommand<ErrorOr<User>>;