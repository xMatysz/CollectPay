using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	private static string _email = "valid@email.com";
	private static byte[] _password = [1, 2, 3];
	private static byte[] _passwordSalt = [1, 2, 3];

	public static readonly string TestEmail = _email;

	public UserBuilder WithEmail(string email)
	{
		_email = email;
		return this;
	}

	public UserBuilder WithPassword(byte[] password)
	{
		_password = password;
		return this;
	}

	public UserBuilder WithPasswordSalt(byte[] salt)
	{
		_passwordSalt = salt;
		return this;
	}

	public User Build()
	{
		return User.Create(_email, _password, _passwordSalt).Value;
	}
}