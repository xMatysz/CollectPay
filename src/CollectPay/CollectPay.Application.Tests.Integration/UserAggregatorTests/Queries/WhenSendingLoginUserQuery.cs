using CollectPay.Application.UserAggregate.Login;
using CollectPay.Domain.UserAggregate;
using CollectPay.Infrastructure.Services;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.UserAggregatorTests.Queries;

public class WhenSendingLoginUserQuery : IntegrationTestBase
{
	public WhenSendingLoginUserQuery(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_Login_When_UserExist()
	{
		const string password = "pass";
		var passwordData = PreparePasswordResult(password);

		var user = new UserBuilder()
			.WithPassword(passwordData.HashedPassword)
			.WithPasswordSalt(passwordData.HashedPasswordSalt)
			.Build();

		await AssumeEntityInDbAsync(user);


		var query = new LoginUserQuery(user.Email, password);

		var result = await Sender.Send(query);

		result.IsError.Should().BeFalse();
	}

	private static (byte[] HashedPasswordSalt, byte[] HashedPassword) PreparePasswordResult(string password)
	{
		return new PasswordHasher().HashString(password);
	}
}