using CollectPay.Application.Common.Abstraction;

namespace CollectPay.Application.UserAggregate.Login;

public sealed record LoginUserQuery(
	string Email,
	string Password) : IQuery<string>;