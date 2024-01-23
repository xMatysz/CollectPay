using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	private string _email = TestEmail;
	private string _password = TestPassword;
	private string _nickName = TestNick;

	private static string TestEmail => "testEmail@domain.com";
	private static string TestPassword => "password123";
	private static string TestNick => "Tester";

	public UserBuilder WithEmail(string email)
	{
		_email = email;
		return this;
	}

	public UserBuilder WithPassword(string password)
	{
		_password = password;
		return this;
	}

	public UserBuilder WithNick(string nick)
	{
		_nickName = nick;
		return this;
	}

	public User Build()
	{
		return User.Create(_email, _password, _nickName).Value;
	}
}