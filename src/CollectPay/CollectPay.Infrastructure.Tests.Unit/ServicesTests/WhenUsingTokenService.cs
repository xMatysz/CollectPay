using CollectPay.Application.Common.Configuration;
using CollectPay.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CollectPay.Infrastructure.Tests.Unit.ServicesTests;

public class WhenUsingTokenService
{
	[Fact]
	public void Should_GenerateValidToken()
	{
		const string email = "test@email.com";
		const string longTokenKey = "my top secret key which has to be long and long even more long and one more";
		var provider = new SecretProvider
		{
			TokenKey = longTokenKey
		};

		var options = Substitute.For<IOptions<SecretProvider>>();
		options.Value.Returns(provider);
		var sut = new TokenService(options);

		var result = sut.GenerateToken(email);

		result.Should().NotBeNullOrEmpty();
	}
}