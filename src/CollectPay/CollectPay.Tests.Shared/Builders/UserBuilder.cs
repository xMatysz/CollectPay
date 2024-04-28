using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	private static string _email = "valid@email.com";
	private static readonly byte[] _password = [1, 2, 3];
	private static readonly byte[] _passwordSalt = [1, 2, 3];

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