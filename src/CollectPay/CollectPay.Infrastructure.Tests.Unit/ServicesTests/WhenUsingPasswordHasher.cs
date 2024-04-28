using CollectPay.Application.Services;
using CollectPay.Infrastructure.Services;
using FluentAssertions;
using Xunit;

namespace CollectPay.Infrastructure.Tests.Unit.ServicesTests;

public class WhenUsingPasswordHasher
{
	private readonly IPasswordHasher _sut;

	public WhenUsingPasswordHasher()
	{
		_sut = new PasswordHasher();
	}

	[Fact]
	public void ShouldHashString()
	{
		const string testString = "testString123";

		var result = _sut.HashString(testString);

		result.HashedString.Should().NotBeEquivalentTo(testString);
	}

	[Fact]
	public void ShouldValidateHashCorrectly()
	{
		const string value = "testString";

		var hashResult = _sut.HashString(value);

		var result = _sut.ValidateHash(value, hashResult.HashedString, hashResult.Salt);

		result.Should().BeTrue();
	}
}