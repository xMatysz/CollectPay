using CollectPay.Domain.UserAggregate;
using CollectPay.Domain.UserAggregate.Errors;

namespace CollectPay.Domain.UnitTests.UserAggregatorTests.SpecificationTests;

public class UserSpecificationTests
{
	[Fact]
	public void ShouldReturnInvalidEmailMessageWhenEmailIsInvalid()
	{
		const string invalidEmail = "abc";
		const string password = "testPassword";
		const string nickName = "testNick";

		var user = User.Create(invalidEmail, password, nickName);

		user.IsError.Should().BeTrue();
		user.FirstError.Should().Be(UserErrors.InvalidEmail);
	}
}