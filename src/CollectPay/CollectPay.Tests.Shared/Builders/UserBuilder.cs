using CollectPay.Domain.UserAggregate;

namespace CollectPay.Tests.Shared.Builders;

public class UserBuilder
{
	public User Build()
	{
		return User.Create("test@domain.com", "pass123", "Tester").Value;
	}
}