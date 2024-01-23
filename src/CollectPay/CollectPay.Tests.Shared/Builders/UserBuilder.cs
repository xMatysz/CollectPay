using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	private string _email = TestEmail;
	private string _password = TestPassword;
	private string _nickName = TestNickName;

	public static string TestEmail => "testEmail@domain.com";
	public static string TestPassword => "password123";
	public static string TestNickName => "Tester";

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

	public UserBuilder WithNick(string nickName)
	{
		_nickName = nickName;
		return this;
	}

	public User Build()
	{
		return User.Create(_email, _password, _nickName).Value;
	}
}