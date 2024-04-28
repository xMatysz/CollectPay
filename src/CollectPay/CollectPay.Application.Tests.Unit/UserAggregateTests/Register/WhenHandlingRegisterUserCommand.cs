using CollectPay.Application.Services;
using CollectPay.Application.UserAggregate.Register;

namespace CollectPay.Application.Tests.Unit.UserAggregateTests.Register;

public class WhenHandlingRegisterUserCommand
{
	private readonly RegisterUserCommandHandler _sut;
	private readonly IPasswordHasher _hasher;

	public WhenHandlingRegisterUserCommand()
	{
		_hasher = Substitute.For<IPasswordHasher>();
		_sut = new(_hasher);
	}

	[Fact]
	public async Task ShouldReturnErrorWhenEmailIsInvalid()
	{
		const string email = "invalidEmail";
		const string password = "test123";
		var command = new RegisterUserCommand(email, password);

		var result = await _sut.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
	}

	[Fact]
	public async Task ShouldHashPassword()
	{
		const string email = "email@domain.com";
		const string password = "Test123!@#";
		var salt = new byte[] { 3, 2, 1 };
		var hashedPassword = new byte[] { 1, 2, 3 };
		_hasher.HashString(Arg.Is(password))
			.Returns((salt, hashedPassword));

		var command = new RegisterUserCommand(email, password);

		var result = await _sut.Handle(command, CancellationToken.None);

		result.IsError.Should().BeFalse();
		result.Value.Password.Should().BeEquivalentTo(hashedPassword);
	}
}