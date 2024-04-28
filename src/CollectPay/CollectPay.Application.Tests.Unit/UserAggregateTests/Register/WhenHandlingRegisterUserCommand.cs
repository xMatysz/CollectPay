using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Application.UserAggregate.Register;
using CollectPay.Domain.UserAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.UserAggregateTests.Register;

public class WhenHandlingRegisterUserCommand
{
	private readonly RegisterUserCommandHandler _sut;
	private readonly IPasswordHasher _hasher;
	private readonly IUserRepository _userRepository;

	public WhenHandlingRegisterUserCommand()
	{
		_hasher = Substitute.For<IPasswordHasher>();
		_userRepository = Substitute.For<IUserRepository>();
		_sut = new(_hasher, _userRepository);
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
		result.Value.PasswordSalt.Should().BeEquivalentTo(salt);
	}

	[Fact]
	public async Task ShouldReturnErrorWhenUserEmailIsAlreadyRegistered()
	{
		const string email = "email@domain.com";
		const string password = "Test123!@#";

		var existingUser = new UserBuilder()
			.WithEmail(email)
			.Build();

		_userRepository.GetByEmail(Arg.Is(email), CancellationToken.None)
			.Returns(existingUser);

		var command = new RegisterUserCommand(email, password);

		var result = await _sut.Handle(command, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(UserErrors.UserAlreadyExist);
	}
}