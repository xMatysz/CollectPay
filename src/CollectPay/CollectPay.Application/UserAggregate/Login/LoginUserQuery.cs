using CollectPay.Application.Common.Abstraction;
using ErrorOr;

namespace CollectPay.Application.UserAggregate.Login;

public sealed record LoginUserQuery(
	string Email,
	string Password) : IQuery<Success>;