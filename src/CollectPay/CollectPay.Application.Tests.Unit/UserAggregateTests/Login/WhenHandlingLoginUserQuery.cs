using CollectPay.Application.Services;
using CollectPay.Application.UserAggregate.Login;
using CollectPay.Domain.UserAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.UserAggregateTests.Login;

public class WhenHandlingLoginUserQuery : UnitTestBase
{
	private readonly LoginUserQueryHandler _sut;
	private readonly IPasswordHasher _passwordHasher;

	public WhenHandlingLoginUserQuery()
	{
		_passwordHasher = Substitute.For<IPasswordHasher>();

		_sut = new(UserRepository, _passwordHasher);
	}

	[Fact]
	public async Task Should_Success_When_UserExistAndCredentialsAreCorrect()
	{
		var user = new UserBuilder().Build();

		UserRepository
			.GetByEmail(Arg.Is(user.Email), CancellationToken.None)
			.Returns(user);

		var passwordString = user.Password.ToString()!;

		_passwordHasher
			.ValidateHash(Arg.Is(passwordString), Arg.Any<byte[]>(), Arg.Any<byte[]>())
			.Returns(false);

		var query = new LoginUserQuery(user.Email, "test");

		var result = await _sut.Handle(query, CancellationToken.None);

		result.Value.Should().Be(Result.Success);
	}

	[Fact]
	public async Task Should_Fail_When_UserNotExist()
	{
		const string email = "test@email.com";

		var query = new LoginUserQuery(email, null!);

		var result = await _sut.Handle(query, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(UserErrors.InvalidCredentials);
	}

	[Fact]
	public async Task Should_Fail_When_PasswordIsIncorrect()
	{
		var user = new UserBuilder().Build();
		UserRepository
			.GetByEmail(Arg.Is(user.Email), CancellationToken.None)
			.Returns(user);

		const string incorrectPassword = "test";
		_passwordHasher
			.ValidateHash(Arg.Is(incorrectPassword), Arg.Any<byte[]>(), Arg.Any<byte[]>())
			.Returns(false);

		var query = new LoginUserQuery(user.Email, incorrectPassword);

		var result = await _sut.Handle(query, CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(UserErrors.InvalidCredentials);
	}
}