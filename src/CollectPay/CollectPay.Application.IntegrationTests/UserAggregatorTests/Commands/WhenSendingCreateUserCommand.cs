using CollectPay.Application.UserAggregate.Commands.CreateUser;
using ErrorOr;

namespace CollectPay.Application.IntegrationTests.UserAggregatorTests.Commands;

public class WhenSendingCreateUserCommand : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly CreateUserCommandHandler _handler;

	public WhenSendingCreateUserCommand(WebApiFactory factory)
		: base(factory)
	{
		_handler = new CreateUserCommandHandler(UserRepository);
	}

	[Fact]
	public async Task ShouldCreateUser()
	{
		var command = new CreateUserCommand(UserBuilder.TestEmail, UserBuilder.TestPassword, UserBuilder.TestNickName);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.Value.Should().Be(Result.Created);
	}
}