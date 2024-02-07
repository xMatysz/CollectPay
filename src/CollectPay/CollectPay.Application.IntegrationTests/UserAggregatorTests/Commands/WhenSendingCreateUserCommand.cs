using CollectPay.Application.UserAggregate.Commands.CreateUser;
using ErrorOr;

namespace CollectPay.Application.IntegrationTests.UserAggregatorTests.Commands;

public class WhenSendingCreateUserCommand : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly CreateUserCommandHandler _handler;

	public WhenSendingCreateUserCommand(WebApiFactory factory)
		: base(factory)
	{
		_handler = new CreateUserCommandHandler(UserRepository, UnitOfWork, HashPasswordService);
	}

	[Fact]
	public async Task ShouldCreateUser()
	{
		var command = new CreateUserCommand(UserBuilder.TestEmail, UserBuilder.TestPassword, UserBuilder.TestNickName);

		var result = await _handler.Handle(command, CancellationToken.None);

		result.IsError.Should().BeFalse();
		result.Should().BeOfType<ErrorOr<CreateUserCommandResult>>();
	}

	[Fact]
	public async Task ShouldModifyPassword()
	{
		var inputPassword = UserBuilder.TestPassword;
		var command = new CreateUserCommand(UserBuilder.TestEmail, inputPassword, UserBuilder.TestNickName);

		var result = await _handler.Handle(command, CancellationToken.None);
		var user = await UserRepository.GetByIdAsync(result.Value.UserId, CancellationToken.None);

		user.Should().NotBeNull();
		user!.Password.Should().NotBeEquivalentTo(inputPassword);
	}
}