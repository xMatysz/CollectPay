using CollectPay.Application.UserAggregate.Register;
using CollectPay.Domain.UserAggregate;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.UserAggregatorTests.Commands;

public class WhenSendingRegisterUserCommand : IntegrationTestBase
{
	public WhenSendingRegisterUserCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_AddUserToRepository()
	{
		const string expectedEmail = "test@string.com";
		const string password = "test";
		var command = new RegisterUserCommand(expectedEmail, password);

		await Sender.Send(command);

		var recordsInDb = await GetAllFromDb<User>();
		recordsInDb.Should().HaveCount(1);

		var userFromDb = recordsInDb.Single();
		userFromDb.Id.Should().NotBeEmpty();
		userFromDb.Email.Should().Be(expectedEmail);
	}
}