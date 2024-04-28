using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	private static string _email = "valid@email.com";
	private static readonly object _password = "test";
	private static readonly object _passwordSalt = "test";

	public static readonly string TestEmail = _email;

	public UserBuilder WithEmail(string email)
	{
		_email = email;
		return this;
	}

	public User Build()
	{
		return User.Create(_email, _password, _passwordSalt).Value;
	}
}